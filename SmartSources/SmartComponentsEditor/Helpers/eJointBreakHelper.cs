using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(JointBreakHelper))]
    public class eJointBreakHelper : eEditor<JointBreakHelper>
    {
        protected override void DrawComponent()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onBreakEvent)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.writeToLog)));
        }
    }
}