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
    [CustomEditor(typeof(FieldValueHelper))]
    public class eFieldValueHelper : eEditorMany<FieldValueHelper>
    {
        private Object _cachedTarget;
        private FieldInfo[] _cachedFields;

        protected override void DrawComponent()
        {
            eCustomEditors.DrawObjectProperty(serializedObject.FindProperty(nameof(_target.target)));
            eCustomEditors.DrawProperty(eGUI.purpleLt, serializedObject.FindProperty(nameof(_target.fieldType)));
            if (!DrawComboEditor()) EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.fieldName)));
        }

        private bool DrawComboEditor()
        {
            if (_target == null) return false;

            if (_cachedTarget != _target.target)
            {
                _cachedTarget = _target.target;
                _cachedFields = _target.target.Reflection_Fields().ToArray();
            }

            if (_cachedFields == null) return false;

            var items = _cachedFields.Where(f => f.FieldType == GetFiledType()).Select(f => f.Name).ToArray();
            if (items.Length == 0) return false;

            eGUI.ComboEditor(ref _target.fieldName, "Field", items);

            return true;
        }

        private Type GetFiledType()
        {
            switch (_target.fieldType)
            {
                case FieldValueHelper.PropertyType.Color: return typeof(Color);
                case FieldValueHelper.PropertyType.Vector2: return typeof(Vector2);
                case FieldValueHelper.PropertyType.Vector3: return typeof(Vector3);
                case FieldValueHelper.PropertyType.Vector4: return typeof(Vector4);
            }
            return null;
        }
    }
}
