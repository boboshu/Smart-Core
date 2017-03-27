using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(ToggleHelper))]
    public class eToggleHelper : eEditorMany<ToggleHelper>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawBoolean(serializedObject.FindProperty(nameof(_target.state)), EditorApplication.isPlaying ? "Current State" : "Initial State");
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onTrue)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onFalse)));
        }
    }
}
