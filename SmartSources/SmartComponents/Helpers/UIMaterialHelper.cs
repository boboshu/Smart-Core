using System;
using Smart.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/UI Material Helper")]
    public class UIMaterialHelper : RedirectableHelper
    {
        //-------------------------------------------------------------------------------

        public enum PropertyType { Color, Vector, Float, Range, Texture }

        public PropertyType propertyType;
        public string propertyName;

        //-------------------------------------------------------------------------------

        private bool _instantiated;

        public void Enum(Action<Material> action)
        {
            Enum<Graphic>(mr =>
            {
                if (mr.material == null) return;
                if (!_instantiated) mr.material = Instantiate(mr.material);                
                action(mr.material);
            });
            _instantiated = true;
        }

        public void EnumInEditor(Action<Material> action)
        {
            Enum<Graphic>(mr =>
            {
                var material = mr.material;
                if (material) action(material);
            });
        }

        //-------------------------------------------------------------------------------

        public void SetColor(Color value)
        {
            if (propertyType == PropertyType.Color) Enum(m => m.SetColor(propertyName, value));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetColor with property type " + propertyType);
        }

        public void SetColor(string value)
        {
            if (propertyType == PropertyType.Color) Enum(m => m.SetColor(propertyName, value.AsHexColor()));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetColor with property type " + propertyType);
        }

        public void SetFloat(float value)
        {
            if (propertyType == PropertyType.Float) Enum(m => m.SetFloat(propertyName, value));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetFloat with property type " + propertyType);
        }

        public void SetTexture(Texture value)
        {
            if (propertyType == PropertyType.Texture) Enum(m => m.SetTexture(propertyName, value));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetTexture with property type " + propertyType);
        }

        public void SetVector2(Vector2 value)
        {
            if (propertyType == PropertyType.Vector) Enum(m => m.SetVector(propertyName, value));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetVector2 with property type " + propertyType);
        }

        public void SetVector3(Vector3 value)
        {
            if (propertyType == PropertyType.Vector) Enum(m => m.SetVector(propertyName, value));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetVector3 with property type " + propertyType);
        }

        public void SetVector4(Vector4 value)
        {
            if (propertyType == PropertyType.Vector) Enum(m => m.SetVector(propertyName, value));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetVector4 with property type " + propertyType);
        }

        //-------------------------------------------------------------------------------
    }
}