using System;
using Smart.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Property Value Helper")]
    public class PropertyValueHelper : MonoBehaviour
    {
        //-------------------------------------------------------------------------------

        public Object target;
        public enum PropertyType { Vector2, Vector3, Vector4, Color }
        public PropertyType propertyType;
        public string propertyName;

        //-------------------------------------------------------------------------------

        private void UpdateProperty<T>(Func<T, T> action)
        {
            if (target == null)
            {
                Debug.LogError("PropertyValueHelper on " + gameObject.name + " - Target is not specified!");
                return;
            }

            var prop = target.Reflection_Property(propertyName);
            if (prop == null)
            {
                Debug.LogError("PropertyValueHelper on " + gameObject.name + " - Property " + propertyName + " is not specified!");
                return;
            }

            prop.SetValue(target, action((T)prop.GetValue(target, null)), null);
        }

        private void SetProperty<T>(Func<T> action)
        {
            if (target == null)
            {
                Debug.LogError("PropertyValueHelper on " + gameObject.name + " - Target is not specified!");
                return;
            }

            var prop = target.Reflection_Property(propertyName);
            if (prop == null)
            {
                Debug.LogError("PropertyValueHelper on " + gameObject.name + " - Property " + propertyName + " is not specified!");
                return;
            }

            prop.SetValue(target, action(), null);
        }

        //-------------------------------------------------------------------------------

        public void SetColorR(float value)
        {
            UpdateProperty<Color>(c => { c.r = value; return c; });
        }

        public void SetColorG(float value)
        {
            UpdateProperty<Color>(c => { c.g = value; return c; });
        }

        public void SetColorB(float value)
        {
            UpdateProperty<Color>(c => { c.b = value; return c; });
        }

        public void SetColorA(float value)
        {
            UpdateProperty<Color>(c => { c.a = value; return c; });
        }

        public void ClearColor()
        {
            UpdateProperty<Color>(c => Color.clear);
        }

        //-------------------------------------------------------------------------------

        public void SetVectorX(float value)
        {
            UpdateProperty<Vector3>(v => { v.x = value; return v; });
        }

        public void SetVectorY(float value)
        {
            UpdateProperty<Vector3>(v => { v.y = value; return v; });
        }

        public void SetVectorZ(float value)
        {
            UpdateProperty<Vector3>(v => { v.y = value; return v; });
        }

        public void SetVectorW(float value)
        {
            UpdateProperty<Vector4>(v => { v.w = value; return v; });
        }

        //-------------------------------------------------------------------------------

        public void ClearVector()
        {
            SetProperty(() => Vector4.zero);
        }

        public void SetOnlyVectorX(float value)
        {
            SetProperty(() => new Vector3(value, 0, 0));
        }

        public void SetOnlyVectorY(float value)
        {
            SetProperty(() => new Vector3(0, value, 0));
        }

        public void SetOnlyVectorZ(float value)
        {
            SetProperty(() => new Vector3(0, 0, value));
        }

        public void SetOnlyVectorW(float value)
        {
            SetProperty(() => new Vector4(0, 0, 0, value));
        }

        //-------------------------------------------------------------------------------
    }
}