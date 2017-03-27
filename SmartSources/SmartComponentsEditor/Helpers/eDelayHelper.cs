using Smart.Custom;
using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Helpers
{
    [CustomEditor(typeof(DelayHelper))]
    public class eDelayHelper : eEditorMany<DelayHelper>
    {
        protected override void DrawComponent()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.seconds)));
            eCustomEditors.DrawProperty(eGUI.red, serializedObject.FindProperty(nameof(_target.runOnStart)));
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onAfterDelay)));            
        }
    }
}
