using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(InputFieldHelper))]
    public class eInputFieldHelper : eEditorMany<InputFieldHelper>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawBoolean(serializedObject.FindProperty(nameof(_target.allowEmpty)));
            eCustomEditors.DrawBoolean(serializedObject.FindProperty(nameof(_target.onlyIfEnterPressed)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onSubmit)));
        }
    }
}
