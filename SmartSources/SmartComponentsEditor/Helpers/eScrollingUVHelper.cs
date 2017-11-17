using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(ScrollingUVHelper))]
    public class eScrollingUVHelper : eEditorMany<ScrollingUVHelper>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.redLt, serializedObject.FindProperty(nameof(_target.redirect)));
            eCustomEditors.DrawProperty(eGUI.greenLt, serializedObject.FindProperty(nameof(_target.enumInChildren)));

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.materialIndex)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.uvAnimationRate)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.textureName)));
        }
    }
}