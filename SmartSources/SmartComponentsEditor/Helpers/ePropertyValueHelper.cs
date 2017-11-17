using System;
using System.Linq;
using System.Reflection;
using Smart.Custom;
using Smart.Editors;
using Smart.Extensions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Smart.Helpers
{
    [CustomEditor(typeof(PropertyValueHelper))]
    public class ePropertyValueHelper : eEditorMany<PropertyValueHelper>
    {
        private Object _cachedTarget;
        private PropertyInfo[] _cachedProperties;

        protected override void DrawComponent()
        {
            eCustomEditors.DrawObjectProperty(serializedObject.FindProperty(nameof(_target.target)));
            eCustomEditors.DrawProperty(eGUI.purpleLt, serializedObject.FindProperty(nameof(_target.propertyType)));
            if (!DrawComboEditor()) EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.propertyName)));
        }

        private bool DrawComboEditor()
        {
            if (_target == null) return false;

            if (_cachedTarget != _target.target)
            {
                _cachedTarget = _target.target;
                _cachedProperties = _target.target.Reflection_Properties().ToArray();
            }

            if (_cachedProperties == null) return false;

            var items = _cachedProperties.Where(f => f.PropertyType == GetPropertyType()).Select(f => f.Name).ToArray();
            if (items.Length == 0) return false;

            eGUI.ComboEditor(ref _target.propertyName, "Property", items);
            return true;
        }

        private Type GetPropertyType()
        {
            switch (_target.propertyType)
            {
                case PropertyValueHelper.PropertyType.Color: return typeof(Color);
                case PropertyValueHelper.PropertyType.Vector2: return typeof(Vector2);
                case PropertyValueHelper.PropertyType.Vector3: return typeof(Vector3);
                case PropertyValueHelper.PropertyType.Vector4: return typeof(Vector4);
            }
            return null;
        }
    }
}
