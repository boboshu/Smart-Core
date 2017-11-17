using System;
using Smart.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Field Value Helper")]
    public class FieldValueHelper : MonoBehaviour
    {
        //-------------------------------------------------------------------------------

        public Object target;
        public enum PropertyType { Vector2, Vector3, Vector4, Color }
        public PropertyType fieldType;
        public string fieldName;

        //-------------------------------------------------------------------------------

        private void UpdateField<T>(Func<T, T> action)
        {
            if (target == null)
            {
                Debug.LogError("FieldValueHelper on " + gameObject.name + " - Target is not specified!");
                return;
            }

            var fld = target.Reflection_Field(fieldName);
            if (fld == null)
            {
                Debug.LogError("FieldValueHelper on " + gameObject.name + " - Field " + fieldName + " is not specified!");
                return;
            }

            fld.SetValue(target, action((T)fld.GetValue(target)));
        }

        private void SetField<T>(Func<T> action)
        {
            if (target == null)
            {
                Debug.LogError("FieldValueHelper on " + gameObject.name + " - Target is not specified!");
                return;
            }

            var fld = target.Reflection_Field(fieldName);
            if (fld == null)
            {
                Debug.LogError("FieldValueHelper on " + gameObject.name + " - Field " + fieldName + " is not specified!");
                return;
            }

            fld.SetValue(target, action());
        }

        //-------------------------------------------------------------------------------

        public void SetColorR(float value)
        {
            UpdateField<Color>(c => { c.r = value; return c; });
        }

        public void SetColorG(float value)
        {
            UpdateField<Color>(c => { c.g = value; return c; });
        }

        public void SetColorB(float value)
        {
            UpdateField<Color>(c => { c.b = value; return c; });
        }

        public void SetColorA(float value)
        {
            UpdateField<Color>(c => { c.a = value; return c; });
        }

        public void ClearColor()
        {
            UpdateField<Color>(c => Color.clear);
        }

        //-------------------------------------------------------------------------------

        public void SetVectorX(float value)
        {
            UpdateField<Vector3>(v => { v.x = value; return v; });
        }

        public void SetVectorY(float value)
        {
            UpdateField<Vector3>(v => { v.y = value; return v; });
        }

        public void SetVectorZ(float value)
        {
            UpdateField<Vector3>(v => { v.y = value; return v; });
        }

        public void SetVectorW(float value)
        {
            UpdateField<Vector4>(v => { v.w = value; return v; });
        }

        //-------------------------------------------------------------------------------

        public void ClearVector()
        {
            SetField(() => Vector4.zero);
        }

        public void SetOnlyVectorX(float value)
        {
            SetField(() => new Vector3(value, 0, 0));
        }

        public void SetOnlyVectorY(float value)
        {
            SetField(() => new Vector3(0, value, 0));
        }

        public void SetOnlyVectorZ(float value)
        {
            SetField(() => new Vector3(0, 0, value));
        }

        public void SetOnlyVectorW(float value)
        {
            SetField(() => new Vector4(0, 0, 0, value));
        }

        //-------------------------------------------------------------------------------
     }
}