using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(CollisionTriggerHelper))]
    public class eCollisionTriggerHelper : eEditor<CollisionTriggerHelper>
    {
        protected override void DrawComponent()
        {
            _target.requireTag = EditorGUILayout.TagField("Require Tag", _target.requireTag);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onEnter)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onExit)));
        }
    }
}