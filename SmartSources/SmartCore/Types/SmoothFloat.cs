using System;
using UnityEngine;

namespace Smart.Types
{
    public class SmoothFloat
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public SmoothFloat(float initialValue = 0, float speed = 1, Action<float> onUpdate = null)
        {
            _realValue = _targetValue = initialValue;
            _speed = speed;
            _onUpdate = onUpdate;
        }

        private readonly Action<float> _onUpdate;
        private readonly float _speed;

        private float _targetValue;
        private float _realValue;

        //--------------------------------------------------------------------------------------------------------------------------

        public void Update()
        {
            if (_targetValue == _realValue) return;

            if (_targetValue > _realValue)
            {
                _realValue += _speed * Time.deltaTime;
                if (_realValue > _targetValue) _realValue = _targetValue;
            }
            else
            {
                _realValue -= _speed * Time.deltaTime;
                if (_realValue < _targetValue) _realValue = _targetValue;
            }

            _onUpdate?.Invoke(_realValue);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void Set(float value)
        {
            _targetValue = value;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
