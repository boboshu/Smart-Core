using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(CountConditionHelper))]
    public class eCountConditionHelper : eEditorMany<CountConditionHelper>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.redLt, serializedObject.FindProperty(nameof(_target.counter)));
            eCustomEditors.DrawProperty(eGUI.limeLt, serializedObject.FindProperty(nameof(_target.condition)));
            eCustomEditors.DrawProperty(eGUI.redDk, serializedObject.FindProperty(nameof(_target.argument)));

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onConditionTrue)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onConditionFalse)));
        }
    }
}
