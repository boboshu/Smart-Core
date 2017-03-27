using System;
using System.Collections.Generic;

namespace Smart.Types
{
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public class EventListener : EventListener<Action>
    {
        protected override void OnExecuteListener(Action listener)
        {
            listener();
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public abstract class EventListener<T>
    {
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private readonly List<T> _listeners = new List<T>();
        private readonly List<T> _changedListeners = new List<T>();
        private bool _locked;
        private bool _changedDuringLock;

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void Add(T listener)
        {
            var listeners = GetEditableListeners();
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void Remove(T listener)
        {
            var listeners = GetEditableListeners();
            listeners.Remove(listener);
        }

        public void Remove(Func<T,bool> filter)
        {
            var listeners = GetEditableListeners();
            listeners.RemoveAll(x => filter(x));
        }

        public IEnumerable<T> Enum => _listeners;

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private List<T> GetEditableListeners()
        {
            if (_locked)
            {
                if (!_changedDuringLock)
                {
                    _changedDuringLock = true;
                    _changedListeners.Clear();
                    _changedListeners.AddRange(_listeners);
                }
                return _changedListeners;
            }
            return _listeners;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void Execute()
        {
            // 1. Execute all events
            _locked = true;
            {
                foreach (var listener in _listeners) OnExecuteListener(listener);
            }
            _locked = false;

            // 2. Apply changes during lock
            if (_changedDuringLock)
            {
                _changedDuringLock = false;
                _listeners.Clear();
                _listeners.AddRange(_changedListeners);
                _changedListeners.Clear();
            }
        }

        protected abstract void OnExecuteListener(T listener);

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}