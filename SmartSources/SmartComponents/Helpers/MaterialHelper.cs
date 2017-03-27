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

        private void WithMaterial(Action<Material> action)
        {
            var go = TargetGameObject;
            if (go == null) return;

            var cmp = go.GetComponent<MeshRenderer>();
            if (cmp == null) return;

            var materials = useSharedMaterials ? cmp.sharedMaterials : cmp.materials;
            var material = materials[materialIndex];

            if (material) action?.Invoke(material);
        }

        public void WithMaterialEditor(Action<Material> action, Action notFoundAction)
        {
            var go = TargetGameObject;
            if (go == null) return;

            var cmp = go.GetComponent<MeshRenderer>();
            if (cmp == null) { notFoundAction?.Invoke(); return; }

            var materials = cmp.sharedMaterials;
            var material = materials[materialIndex];

            if (material) action?.Invoke(material);
            else  notFoundAction?.Invoke();
        }

        //-------------------------------------------------------------------------------

        public void SetColor(Color value)
        {
            if (propertyType == PropertyType.Color) WithMaterial(m => m.SetColor(propertyName, value));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetColor with property type " + propertyType);
        }

        public void SetColor(string value)
        {
            if (propertyType == PropertyType.Color) WithMaterial(m => m.SetColor(propertyName, value.AsHexColor()));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetColor with property type " + propertyType);
        }

        public void SetFloat(float value)
        {
            if (propertyType == PropertyType.Float) WithMaterial(m => m.SetFloat(propertyName, value));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetFloat with property type " + propertyType);
        }

        public void SetTexture(Texture value)
        {
            if (propertyType == PropertyType.Texture) WithMaterial(m => m.SetTexture(propertyName, value));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetTexture with property type " + propertyType);
        }

        public void SetVector2(Vector2 value)
        {
            if (propertyType == PropertyType.Vector) WithMaterial(m => m.SetVector(propertyName, value));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetVector2 with property type " + propertyType);
        }

        public void SetVector3(Vector3 value)
        {
            if (propertyType == PropertyType.Vector) WithMaterial(m => m.SetVector(propertyName, value));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetVector3 with property type " + propertyType);
        }

        public void SetVector4(Vector4 value)
        {
            if (propertyType == PropertyType.Vector) WithMaterial(m => m.SetVector(propertyName, value));
            else Debug.LogError("MaterialHelper on " + gameObject.name + " - can`t SetVector4 with property type " + propertyType);
        }

        //-------------------------------------------------------------------------------
    }
}