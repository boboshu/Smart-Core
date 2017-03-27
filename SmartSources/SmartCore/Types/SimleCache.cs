using System;
using System.Collections.Generic;

namespace Smart.Types
{
    public class SimleCache<K,V>
    {
        //--------------------------------------------------------------------------------------------------------------------------

        private readonly Dictionary<K, V> _cachedValues = new Dictionary<K, V>();
        private readonly Func<K, V> _getter;

        //--------------------------------------------------------------------------------------------------------------------------

        public SimleCache(Func<K, V> getter)
        {
            _getter = getter;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public V this[K key]
        {
            get
            {
                V v;
                if (_cachedValues.TryGetValue(key, out v)) return v;                
                _cachedValues.Add(key, v = _getter(key));
                return v;
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
