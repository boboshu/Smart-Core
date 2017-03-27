using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(ScrollingUVHelper))]
    public class eScrollingUVHelper : eEditorMany<ScrollingUVHelper>
    {
        protected override void DrawComponent()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.materialIndex)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.uvAnimationRate)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.textureName)));
        }
    }
}