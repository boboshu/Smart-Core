using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(IntervalEventHelper))]
    public class eIntervalEventHelper : eEditorMany<IntervalEventHelper>
    {
        protected override void DrawComponent()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.interval)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onInterval)));
        }
    }
}
