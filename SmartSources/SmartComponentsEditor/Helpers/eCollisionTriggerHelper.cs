using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(CollisionTriggerHelper))]
    public class eCollisionTriggerHelper : eEditor<CollisionTriggerHelper>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.redLt, serializedObject.FindProperty(nameof(_target.requireGameObject)));
            _target.requireTag = _target.requireGameObject == null ? EditorGUILayout.TagField("Require Tag", _target.requireTag) : "Untagged";

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onEnter)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onExit)));
        }
    }
}