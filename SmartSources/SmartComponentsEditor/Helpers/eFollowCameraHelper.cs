using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(FollowCameraHelper))]
    public class eFollowCameraHelper : eEditor<FollowCameraHelper>
    {
        protected override void DrawComponent()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.syncPosition)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.syncRotation)));
        }
    }
}