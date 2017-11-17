using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(TypeConverterHelper))]
    public class eTypeConverterHelper : eEditorMany<TypeConverterHelper>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.red, serializedObject.FindProperty(nameof(_target.convertToType)));            
            switch (_target.convertToType)
            {
                case TypeConverterHelper.ConvertToType.Integer:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.applyInteger)));
                    return;

                case TypeConverterHelper.ConvertToType.Float:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.applyFloat)));
                    return;

                case TypeConverterHelper.ConvertToType.Boolean:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.applyBoolean)));
                    return;

                case TypeConverterHelper.ConvertToType.String:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.applyString)));
                    return;
            }
        }
    }
}
