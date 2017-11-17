using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(SelectIntervalHelper))]
    public class eSelectIntervalHelper : eEditorMany<SelectIntervalHelper>
    {
        protected override void DrawComponent()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.repeatEvents)));

            eGUI.CollectionToolbar(ref _target.intervals, () => new SelectIntervalHelper.Interval(), "Intervals");
            eGUI.PropCollection(serializedObject.FindProperty(nameof(_target.intervals)), ref _target.intervals,
                (v, p, i) =>
                {
                    EditorGUILayout.PropertyField(p.FindPropertyRelative(nameof(v.minimum)));
                    EditorGUILayout.PropertyField(p.FindPropertyRelative(nameof(v.maximum)));
                    EditorGUILayout.PropertyField(p.FindPropertyRelative(nameof(v.onEvent)));
                }, eCollection.Delete | eCollection.Reorder | eCollection.VerticalTools);
        }
    }
}
