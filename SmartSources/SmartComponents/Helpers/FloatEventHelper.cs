using System;
using Smart.Extensions;
using Smart.Types;
using UnityEngine;
using UnityEngine.Events;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Float Event Helper")]
    public class FloatEventHelper : RedirectableHelper
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public enum FloatSource { PositionX, PositionY, PositionZ, RotationX, RotationY, RotationZ, Random01, Custom }
        public FloatSource floatSource;     

        public float sourceMinimum = 0;
        public float sourceMaximum = 1;

        public float normalizedMinimum = 0;
        public float normalizedMaximum = 1;

        public UnityEventFloat onValueChanged = new UnityEventFloat();
        public UnityEvent onValueMinimum = new UnityEvent();
        public UnityEvent onValueMaximum = new UnityEvent();

        public bool clampAngle180;

        private float ClampAngle(float value)
        {
            if (clampAngle180)
            {
                while (value > 180) value -= 360;
                while (value < -180) value += 360;
            }
            return value;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        void Update()
        {
            switch (floatSource)
            {
                case FloatSource.PositionX: ApplyNewValue(TargetGameObject.transform.localPosition.x); break;
                case FloatSource.PositionY: ApplyNewValue(TargetGameObject.transform.localPosition.y); break;
                case FloatSource.PositionZ: ApplyNewValue(TargetGameObject.transform.localPosition.z); break;
                case FloatSource.RotationX: ApplyNewValue(ClampAngle(TargetGameObject.transform.localEulerAngles.x)); break;
                case FloatSource.RotationY: ApplyNewValue(ClampAngle(TargetGameObject.transform.localEulerAngles.y)); break;
                case FloatSource.RotationZ: ApplyNewValue(ClampAngle(TargetGameObject.transform.localEulerAngles.z)); break;
                case FloatSource.Random01: ApplyNewValue(UnityEngine.Random.value); break;
                case FloatSource.Custom: break;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void SetCustomValue(float value)
        {
            if (floatSource != FloatSource.Custom) return;
            ApplyNewValue(value);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private float _value;

        private void ApplyNewValue(float value)
        {
            if (sourceMinimum >= sourceMaximum || normalizedMinimum >= normalizedMaximum)
            {
                Debug.LogError(gameObject.GetFullName() + ": Check FloatEventHelper Minimum and Maximum values!");
                return;
            }

            var v = Mathf.Clamp(value, sourceMinimum, sourceMaximum);
            if (Math.Abs(_value - v) < 0.01f) return;
            _value = v;

            if (v == sourceMinimum)
            {
                onValueMinimum.Invoke();
                onValueChanged.Invoke(normalizedMinimum);
                return;
            }

            if (v == sourceMaximum)
            {
                onValueMaximum.Invoke();
                onValueChanged.Invoke(normalizedMaximum);
                return;
            }
            
            var absoluteValue = (v - sourceMinimum) / (sourceMaximum - sourceMinimum);
            onValueChanged.Invoke(absoluteValue * (normalizedMaximum - normalizedMinimum) + normalizedMinimum);
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
