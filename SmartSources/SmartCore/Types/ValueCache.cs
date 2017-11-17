using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Smart.Types
{
    public class ValueCache<V>
    {
        //--------------------------------------------------------------------------------------------------------------------------

        private V _value;
        private float _nextUpdateTime;
        private Func<V> _getter;
        private readonly float _interval;

        //--------------------------------------------------------------------------------------------------------------------------

        // ±25% to distribute calculations in time, not do everything in the beginning of new interval
        private const float INTERVAL_DEVIATION = 0.25f;
        private const float INTERVAL_MIN = 1 - INTERVAL_DEVIATION;
        private const float INTERVAL_MAX = 1 + INTERVAL_DEVIATION;

        //--------------------------------------------------------------------------------------------------------------------------

        public ValueCache(Func<V> getter, float interval = 1)
        {
            _getter = getter;
            _interval = interval;
        }

        public ValueCache(float interval = 1)
        {
            _getter = () => default(V); // use default getter
            _interval = interval;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void Getter(Func<V> getter)
        {
            _getter = getter;
            _nextUpdateTime = 0; // force getter call on next get
        }

        public void Clear()
        {
            _nextUpdateTime = 0; // force getter call on next get
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public V Value
        {
            get
            {
                if (Time.unscaledTime > _nextUpdateTime)
                {
                    _value = _getter();
                    _nextUpdateTime = Time.unscaledTime + _interval * Random.Range(INTERVAL_MIN, INTERVAL_MAX);
                }
                return _value;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static implicit operator V(ValueCache<V> value)
        {
            return value.Value;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
