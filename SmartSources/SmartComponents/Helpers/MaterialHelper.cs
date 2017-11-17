using System;
using Smart.Extensions;
using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Material Helper")]
    public class MaterialHelper : RedirectableHelper
    {
        //-------------------------------------------------------------------------------

        public enum PropertyType { Color, Vector, Float, Range, Texture }

        public bool useSharedMaterials;
        public int materialIndex;
        public PropertyType propertyType;
        public string propertyName;

        //-------------------------------------------------------------------------------

        public void Enum(Action<Material> action)
        {
            Enum<MeshRenderer>(mr =>
            {
                var materials = useSharedMaterials ? mr.sharedMaterials : mr.materials;
                var material = materials[materialIndex];
                if (material) action(material);
            });
        }

        public void EnumInEditor(Action<Material> action)
        {
            Enum<MeshRenderer>(mr =>
            {
                var materials = mr.sharedMaterials; // force to use shared materials from editor
                var material = materials[materialIndex];
                if (material) action(material);
            });
        }

        //-------------------------------------------------------------------------------

        public void SetColorAlpha(float value)
        {
            if (propertyType == PropertyType.Color) Enum(m => m.SetColor(propertyName, new Color(1, 1, 1, value)));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetColor with property type " + propertyType);
        }

        public void SetColorGray(float value)
        {
            if (propertyType == PropertyType.Color) Enum(m => m.SetColor(propertyName, new Color(value, value, value)));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetColor with property type " + propertyType);
        }

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
            if (propertyType == PropertyType.Float || propertyType == PropertyType.Range) Enum(m => m.SetFloat(propertyName, value));
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