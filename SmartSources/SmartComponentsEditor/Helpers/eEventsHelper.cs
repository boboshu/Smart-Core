using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Helpers
{
    [CustomEditor(typeof(EventsHelper))]
    public class eEventsHelper : eEditorMany<EventsHelper>
    {
        protected override void DrawComponent()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onEnable)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onDisable)));
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onStart)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onDestroy)));
        }
    }
}
