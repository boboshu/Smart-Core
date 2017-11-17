using System.Linq;
using Smart.Custom;
using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Helpers
{
    [CustomEditor(typeof(MaterialHelper))]
    public class eMaterialHelper : eEditorMany<MaterialHelper>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.redLt, serializedObject.FindProperty(nameof(_target.redirect)));
            eCustomEditors.DrawProperty(eGUI.greenLt, serializedObject.FindProperty(nameof(_target.enumInChildren)));

            eCustomEditors.DrawProperty(_target.useSharedMaterials ? eGUI.limeLt : eGUI.redLt, serializedObject.FindProperty(nameof(_target.useSharedMaterials)));
            eCustomEditors.DrawProperty(_target.materialIndex == 0 ? eGUI.limeLt : eGUI.redLt, serializedObject.FindProperty(nameof(_target.materialIndex)));

            eCustomEditors.DrawProperty(eGUI.purpleLt, serializedObject.FindProperty(nameof(_target.propertyType)));

            _materialNotFound = true;
            _target.EnumInEditor(DrawMaterial);

            if (_materialNotFound)
            {
                EditorGUILayout.HelpBox("Material not found", MessageType.Error);
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.propertyName)));
            }
        }

        private bool _materialNotFound;
        private Shader _cachedShader;
        private ShaderPropertyInfo[] _cachedProperties;

        private class ShaderPropertyInfo
        {
            public string name;
            public ShaderUtil.ShaderPropertyType type;
        }

        private void DrawMaterial(Material m)
        {
            if (!_materialNotFound) return; // draw only first material

            _materialNotFound = false;
            eGUI.LabelBold(m.name);

            var shader = m.shader;
            if (shader == null) return;

            if (shader != _cachedShader)
            {
                _cachedShader = shader;
                _cachedProperties = new ShaderPropertyInfo[ShaderUtil.GetPropertyCount(shader)];
                for (var i = 0; i < _cachedProperties.Length; i++)
                    _cachedProperties[i] = new ShaderPropertyInfo { name = ShaderUtil.GetPropertyName(shader, i), type = ShaderUtil.GetPropertyType(shader, i) };
            }

            eGUI.ComboEditor(ref _target.propertyName, "Property", _cachedProperties.Where(p => (byte)p.type == (byte)_target.propertyType).Select(p => p.name).ToArray());
        }
    }
}
