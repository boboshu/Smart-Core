using System;
using System.Collections.Generic;

namespace Smart.Types
{
    public class TinyCyclicCache<K,V>
    {
        //--------------------------------------------------------------------------------------------------------------------------

        private readonly K[] _cyclicKeys;
        private readonly V[] _cyclicValues;
        private readonly int[] _cyclicHashes;
        
        private readonly Func<K, V> _getter;
        private readonly EqualityComparer<K> _equalityComparer;

        private int _nextIndex;
        private readonly int _size;        
        private int _count;

        //--------------------------------------------------------------------------------------------------------------------------

        public bool IsEmpty => _count == 0;

        public bool IsNotEmpty => _count > 0;

        //--------------------------------------------------------------------------------------------------------------------------

        public TinyCyclicCache(Func<K, V> getter, int size = 10)
        {
            _count = 0;
            _size = size;            
            _getter = getter;
            _cyclicKeys = new K[size];
            _cyclicValues = new V[size];
            _cyclicHashes = new int[size];
            _equalityComparer = EqualityComparer<K>.Default;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public V this[K key]
        {
            get
            {
                var keyHash = key.GetHashCode();
                for (var i = 0; i < _count; i++)
                {
                    if (_cyclicHashes[i] == keyHash)
                    {
                        if (_equalityComparer.Equals(_cyclicKeys[i], key))
                            return _cyclicValues[i];
                    }
                }

                var value = _getter(key);
                var index = _nextIndex++ % _size;

                _cyclicKeys[index] = key;
                _cyclicValues[index] = value;
                _cyclicHashes[index] = keyHash;

                if (_count < _size) _count++;
                return value;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<K> EnumKeys
        {
            get
            {
                for (var i = 0; i < _count; i++)
                    yield return _cyclicKeys[i];
            }
        }

        public IEnumerable<V> EnumValues
        {
            get
            {
                for (var i = 0; i < _count; i++)
                    yield return _cyclicValues[i];
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void Clear()
        {
            for (var i = 0; i < _count; i++)
            {
                _cyclicKeys[i] = default(K);
                _cyclicValues[i] = default(V);
                _cyclicHashes[i] = 0;
            }

            _nextIndex = 0;
            _count = 0;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
