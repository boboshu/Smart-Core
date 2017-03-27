using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(ColorLerpHelper))]
    public class eColorLerpHelper : eEditor<ColorLerpHelper>
    {
        protected override void DrawComponent()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.duration)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.current)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.target)));

            eCustomEditors.DrawProperty(eGUI.yellow, serializedObject.FindProperty(nameof(_target.onStart)));
            eCustomEditors.DrawProperty(eGUI.yellow, serializedObject.FindProperty(nameof(_target.onLerp)));
            eCustomEditors.DrawProperty(eGUI.yellow, serializedObject.FindProperty(nameof(_target.onFinish)));
        }
    }
}