using UnityEngine;

namespace Smart.Types
{
    public struct BooleanState
    {
        //--------------------------------------------------------------------------------------------------------------------------

        private bool _value;
        private float _lastTime;

        public BooleanState(bool defaultValue = false) : this()
        {
            _value = defaultValue;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void ResetDuration()
        {
            _lastTime = Time.time;
        }

        public void Set(bool v)
        {
            if (_value == v) return;
            _lastTime = Time.time;
            _value = v;
        }

        public bool IsChanged => Time.time == _lastTime;

        //--------------------------------------------------------------------------------------------------------------------------

        public float TrueDuration => _value ? Time.time - _lastTime : 0;

        public float FalseDuration => _value ? 0 : Time.time - _lastTime;

        //--------------------------------------------------------------------------------------------------------------------------

        public static implicit operator bool(BooleanState v)
        {
            return v._value;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
