using System.Collections.Generic;

namespace Smart.Types
{
    public class TinyCyclicValues<V>
    {
        //--------------------------------------------------------------------------------------------------------------------------

        private readonly V[] _cyclicValues;

        private int _nextIndex;
        private readonly int _size;        
        private int _count;

        //--------------------------------------------------------------------------------------------------------------------------

        public bool IsEmpty => _count == 0;

        public bool IsNotEmpty => _count > 0;

        public int Count => _count;

        //--------------------------------------------------------------------------------------------------------------------------

        public TinyCyclicValues(int size = 20)
        {
            _count = 0;
            _size = size;
            _cyclicValues = new V[size];
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public bool HasNext => _count == _size;

        public V Next()
        {
            return _cyclicValues[_nextIndex++ % _size];
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void Push(V value)
        {
            _cyclicValues[_nextIndex++ % _size] = value;
            if (_count < _size) _count++;
        }

        public V Pop()
        {
            if (_count == 0) return default(V);

            _count--;
            var index = --_nextIndex % _size;
            var value = _cyclicValues[index];
            _cyclicValues[index] = default(V); // clear reference
            return value;
        }

        public V Peek()
        {
            return _count == 0 ? default(V) : _cyclicValues[(_nextIndex - 1) % _size];
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<V> Enum
        {
            get
            {
                for (var i = 0; i < _count; i++)
                    yield return _cyclicValues[i];
            }
        }

        public IEnumerable<V> EnumFromFirst
        {
            get
            {
                for (var i = _nextIndex - _count; i < _nextIndex; i++)
                    yield return _cyclicValues[i % _size];
            }
        }

        public IEnumerable<V> EnumFromLast
        {
            get
            {
                for (var i = _nextIndex - 1; i >= _nextIndex - _count; i--)
                    yield return _cyclicValues[i % _size];
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void Clear()
        {
            for (var i = 0; i < _count; i++)
                _cyclicValues[i] = default(V);

            _nextIndex = 0;
            _count = 0;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
