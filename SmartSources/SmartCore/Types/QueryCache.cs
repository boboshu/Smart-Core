using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Smart.Types
{
    public class QueryCache<K,V>
    {
        //--------------------------------------------------------------------------------------------------------------------------

        private class CachedData
        {
            public V value;
            public float nextUpdateTime;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private readonly Dictionary<K, CachedData> _cachedValues = new Dictionary<K, CachedData>();
        private readonly Func<K, V> _getter;
        private readonly float _interval;

        //--------------------------------------------------------------------------------------------------------------------------

        // ±25% to distribute calculations in time, not do everything in the beginning of new interval
        private const float INTERVAL_DEVIATION = 0.25f;
        private const float INTERVAL_MIN = 1 - INTERVAL_DEVIATION;
        private const float INTERVAL_MAX = 1 + INTERVAL_DEVIATION;

        //--------------------------------------------------------------------------------------------------------------------------

        public QueryCache(Func<K, V> getter, float interval = 1)
        {
            _getter = getter;
            _interval = interval;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public V this[K key]
        {
            get
            {
                CachedData d;
                if (_cachedValues.TryGetValue(key, out d))
                {
                    if (Time.unscaledTime > d.nextUpdateTime)
                    {
                        d.value = _getter(key);
                        d.nextUpdateTime = Time.unscaledTime + _interval * Random.Range(INTERVAL_MIN, INTERVAL_MAX);
                    }
                }
                else
                {
                    _cachedValues.Add(key, d = new CachedData());
                    d.value = _getter(key);
                    d.nextUpdateTime = Time.unscaledTime + _interval * Random.Range(INTERVAL_MIN, INTERVAL_MAX);
                }
                return d.value;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void Clear()
        {
            _cachedValues.Clear();
        }

        public void Remove(K key)
        {
            _cachedValues.Remove(key);
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
