using System;
using System.Linq;
using Smart.Types;
using UnityEngine;

namespace Smart.Managers
{
    [ClearTransform]
    [AddComponentMenu("Smart/Managers/Updates Manager")]
    public class UpdatesManager : MonoBehaviour
    {
        //----------------------------------------------------------------------------------------------------------------------------------------------

        public bool dontDestroyOnLoad = true;

        private void Awake()
        {
            if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        private class ListenerData
        {
            public Action action;
            public float interval;
            public float nextUpdateTime;
        }

        private class EventListener_ListenerData : EventListener<ListenerData>
        {
            public float currentTime;

            protected override void OnExecuteListener(ListenerData listener)
            {
                if (listener.nextUpdateTime > currentTime) return;
                listener.nextUpdateTime = currentTime + listener.interval;
                listener.action();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        private class DeferredCallData : ListenerData
        {
            public Func<bool> condition;
        }

        private class EventListener_DeferredCallData : EventListener<DeferredCallData>
        {
            public float currentTime;

            protected override void OnExecuteListener(DeferredCallData listener)
            {
                if (listener.nextUpdateTime > currentTime) return;

                if (listener.condition == null || listener.condition())
                {
                    _deferredCalls.Remove(listener);
                    listener.action();
                }
                else // wait for interval to test condition once more
                {
                    listener.nextUpdateTime = currentTime + listener.interval;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        public static void DeferredCall(Action action, float interval = 0)
        {
            _deferredCalls.Add(new DeferredCallData { action = action, interval = interval, nextUpdateTime = Time.unscaledTime + interval }); // call after interval
        }

        public static void DeferredCall(Func<bool> condition, Action act, float interval = 0.1f)
        {
            _deferredCalls.Add(new DeferredCallData { action = act, interval = interval, nextUpdateTime = Time.unscaledTime + interval, condition = condition });
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        public static void Subscribe(Action action, float interval = 1)
        {
            if (_listeners.Enum.Any(x => x.action == action)) return;
            _listeners.Add(new ListenerData { action = action, interval = interval, nextUpdateTime = Time.unscaledTime }); // call update on next update and than after inverval
        }

        public static void UnSubscribe(Action action)
        {
            _listeners.Remove(x => x.action == action);
        }
        
        public static bool IsSubscribed(Action action)
        {
            return _listeners.Enum.Any(x => x.action == action);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        private static readonly EventListener_ListenerData _listeners = new EventListener_ListenerData();
        private static readonly EventListener_DeferredCallData _deferredCalls = new EventListener_DeferredCallData();

        //----------------------------------------------------------------------------------------------------------------------------------------------

        void Update()
        {
            _deferredCalls.currentTime = _listeners.currentTime = Time.unscaledTime;
            _listeners.Execute();
            _deferredCalls.Execute();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------
    }
}
