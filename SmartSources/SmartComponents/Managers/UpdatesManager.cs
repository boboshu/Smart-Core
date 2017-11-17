//#define DEBUG_INFO
using System;
using System.Collections.Generic;
using System.Linq;
using Smart.Extensions;
using Smart.Types;
using Smart.Utils;
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

        public const float UPS_1 = 1f;
        public const float UPS_2 = 1f / 2;
        public const float UPS_5 = 1f / 5;
        public const float UPS_10 = 1f / 10;
        public const float UPS_20 = 1f / 20;
        public const float UPS_30 = 1f / 30;
        public const float UPS_40 = 1f / 40;
        public const float UPS_50 = 1f / 50;
        public const float UPS_60 = 1f / 60;
        public const float UPS_70 = 1f / 70;
        public const float UPS_80 = 1f / 80;
        public const float UPS_90 = 1f / 90;
        public const float UPS_EACH_FRAME = 0;

        public static readonly float[] UPS = { UPS_1, UPS_2, UPS_5, UPS_10, UPS_20, UPS_30, UPS_40, UPS_50, UPS_60, UPS_70, UPS_80, UPS_90, UPS_EACH_FRAME };
        public static readonly string[] UPS_NAMES = { "1 Update Per Second", "2 Update Per Second", "10 Updates Per Second", "20 Updates Per Second", "30 Updates Per Second", "40 Updates Per Second", "50 Updates Per Second", "60 Updates Per Second", "70 Updates Per Second", "80 Updates Per Second", "90 Updates Per Second", "Update Each Frame" };

        public static readonly Action NULL_ACTION = () => { };

        //----------------------------------------------------------------------------------------------------------------------------------------------

        private class CallData
        {
            public Action action;
            public float interval;
            public float nextUpdateTime;
#if DEBUG_INFO
            public string callstack;
#endif

            public void Execute()
            {
                if (nextUpdateTime > _currentTime) return;
                nextUpdateTime = _currentTime + interval;
                action();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        private class DeferredCallData : CallData
        {
            public Func<bool> condition;

            public bool ConditionalExecute()
            {
                if (nextUpdateTime > _currentTime) return false;

                if (condition == null || condition())
                {                    
                    action();
                    return true;
                }
                
                // wait for interval to test condition once more
                nextUpdateTime = _currentTime + interval;
                return false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        private class NamedDeferredCallData : DeferredCallData
        {
            public string name;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        public static void DeferredCall(string name, Action action, float interval = UPS_EACH_FRAME)
        {
            _namedDeferredCalls.Remove(x => x.name == name);
            _namedDeferredCalls.AddFirst(new NamedDeferredCallData
            {
#if DEBUG_INFO
                callstack = Environment.StackTrace.MakeStackTraceShorter(2),
#endif
                name = name,
                action = action,
                interval = interval,
                nextUpdateTime = Time.unscaledTime + interval
            }); // call after interval
        }

        public static void ConditionalCall(string name, Func<bool> condition, Action action = null, float interval = UPS_10, bool checkImmediately = true)
        {
            _namedDeferredCalls.Remove(x => x.name == name);

            if (checkImmediately && condition())
            {
                (action ?? NULL_ACTION)();
                return;
            }

            _namedDeferredCalls.AddFirst(new NamedDeferredCallData
            {
#if DEBUG_INFO
                callstack = Environment.StackTrace.MakeStackTraceShorter(2),
#endif
                name = name,
                action = action ?? NULL_ACTION,
                interval = interval,
                nextUpdateTime = Time.unscaledTime + interval,
                condition = condition
            });
        }

        public static void RemoveCall(string name)
        {
            _namedDeferredCalls.Remove(x => x.name == name);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        public static void DeferredCall(Action action, float interval = UPS_EACH_FRAME)
        {
            _deferredCalls.AddFirst(new DeferredCallData
            {
#if DEBUG_INFO
                callstack = Environment.StackTrace.MakeStackTraceShorter(2),
#endif
                action = action,
                interval = interval,
                nextUpdateTime = Time.unscaledTime + interval
            }); // call after interval
        }

        public static void ConditionalCall(Func<bool> condition, Action action = null, float interval = UPS_10, bool checkImmediately = true)
        {
            if (checkImmediately && condition())
            {
                (action ?? NULL_ACTION)();
                return;
            }

            _deferredCalls.AddFirst(new DeferredCallData
            {
#if DEBUG_INFO
                callstack = Environment.StackTrace.MakeStackTraceShorter(2),
#endif
                action = action ?? NULL_ACTION,
                interval = interval,
                nextUpdateTime = Time.unscaledTime + interval,
                condition = condition
            });
        }

        public static void ClearPendingConditions()
        {
            _deferredCalls.RemoveAll(x => x.condition != null);
            _namedDeferredCalls.RemoveAll(x => x.condition != null);
        }

        public static void ClearAllCalls()
        {
            _deferredCalls.Clear();
            _namedDeferredCalls.Clear();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        public static void Subscribe(Action action, float interval = UPS_1)
        {
            _subscribers.AddFirst(new CallData
            {
#if DEBUG_INFO
                callstack = Environment.StackTrace.MakeStackTraceShorter(2),
#endif
                action = action,
                interval = interval,
                nextUpdateTime = Time.unscaledTime
            }); // call update on next update and than after inverval
        }

        public static void UnSubscribe(Action action)
        {
            _subscribers.Remove(x => x.action == action);
        }
        
        public static bool IsSubscribed(Action action)
        {
            return _subscribers.Any(x => x.action == action);
        }

        public static void ClearAllSubScriptions()
        {
            _subscribers.Clear();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        private static readonly LinkedList<CallData> _subscribers = new LinkedList<CallData>();
        private static readonly LinkedList<DeferredCallData> _deferredCalls = new LinkedList<DeferredCallData>();
        private static readonly LinkedList<NamedDeferredCallData> _namedDeferredCalls = new LinkedList<NamedDeferredCallData>();

        //----------------------------------------------------------------------------------------------------------------------------------------------

        private static float _currentTime;

        void Update()
        {
            _currentTime = Time.unscaledTime;
            _subscribers.SafeDo(x => x.Execute());
            _deferredCalls.RemoveAll(x => x.ConditionalExecute());
            _namedDeferredCalls.RemoveAll(x => x.ConditionalExecute());
        }

        public static void ShowDebugInfo()
        {
            Debug.Log("========================== Updates Manager Debug Info ==========================");
#if DEBUG_INFO            

            Debug.Log("========================== Subscribers: " + _subscribers.Count + " ==========================");
            foreach (var call in _subscribers) Debug.Log(call.callstack + "\n\n");

            Debug.Log("========================== DeferredCals: " + _deferredCalls.Count + " ==========================");
            foreach (var call in _deferredCalls) Debug.Log(call.callstack + "\n\n");

            Debug.Log("========================== NamedDeferredCals: " + _namedDeferredCalls.Count + " ==========================");
            foreach (var call in _namedDeferredCalls) Debug.Log(call.name + "\n" + call.callstack + "\n\n");
#else
            Debug.Log("Subscribers: " + _subscribers.Count + " DeferredCals: " + _deferredCalls.Count + " NamedDeferredCals: " + _namedDeferredCalls.Count);
            foreach (var call in _namedDeferredCalls) Debug.Log(call.name + "\n\n");
            Debug.Log("For more info use conditional define in UpdatesManager.cs");
#endif
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        public static void Animation(Action<float> onAnimate, float duration = 1, Action onDone = null, float interval = UPS_20, Interpolation.Kind interpolationKind = Interpolation.Kind.Linear)
        {
            var startTime = Time.time;
            ConditionalCall(() =>
            {
                var t = Time.time - startTime;
                if (t >= duration) { onAnimate(1); return true; }
                onAnimate(Interpolation.Apply(interpolationKind, t / duration));
                return false;
            }, onDone, interval);
        }

        public static void Animation(string name, Action<float> onAnimate, float duration = 1, Action onDone = null, float interval = UPS_20, Interpolation.Kind interpolationKind = Interpolation.Kind.Linear)
        {
            var startTime = Time.time;
            ConditionalCall(name, () =>
            {
                var t = Time.time - startTime;
                if (t >= duration) { onAnimate(1); return true; }
                onAnimate(Interpolation.Apply(interpolationKind, t / duration));
                return false;
            }, onDone, interval);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        public static void AnimationLoop(Action<float> onAnimate, float duration = 1, float interval = UPS_20, Interpolation.Kind interpolationKind = Interpolation.Kind.Linear)
        {
            var startTime = Time.time;
            ConditionalCall(() =>
            {
                var t = Time.time - startTime;
                t %= duration;
                onAnimate(Interpolation.Apply(interpolationKind, t / duration));
                return false;
            }, null, interval);
        }

        public static void AnimationLoop(string name, Action<float> onAnimate, float duration = 1, float interval = UPS_20, Interpolation.Kind interpolationKind = Interpolation.Kind.Linear)
        {
            var startTime = Time.time;
            ConditionalCall(name, () =>
            {
                var t = Time.time - startTime;
                t %= duration;
                onAnimate(Interpolation.Apply(interpolationKind, t / duration));
                return false;
            }, null, interval);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        public static void AnimationPingPong(Action<float> onAnimate, float duration = 1, float interval = UPS_20, Interpolation.Kind interpolationKind = Interpolation.Kind.Linear)
        {
            var startTime = Time.time;
            ConditionalCall(() =>
            {
                var t = Time.time - startTime;
                var i = (int) (t / duration);
                t %= duration;
                t = i % 2 == 0 ? t : duration - t;
                onAnimate(Interpolation.Apply(interpolationKind, t / duration));
                return false;
            }, null, interval);
        }

        public static void AnimationPingPong(string name, Action<float> onAnimate, float duration = 1, float interval = UPS_20, Interpolation.Kind interpolationKind = Interpolation.Kind.Linear)
        {
            var startTime = Time.time;
            ConditionalCall(name, () =>
            {
                var t = Time.time - startTime;
                var i = (int) (t / duration);
                t %= duration;
                t = i % 2 == 0 ? t : duration - t;
                onAnimate(Interpolation.Apply(interpolationKind, t / duration));
                return false;
            }, null, interval);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

    }
}
