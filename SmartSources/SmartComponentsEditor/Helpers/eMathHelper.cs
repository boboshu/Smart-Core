using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(MathHelper))]
    public class eMathHelper : eEditorMany<MathHelper>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.blueLt, serializedObject.FindProperty(nameof(_target.operationType)));
            eCustomEditors.DrawProperty(eGUI.redLt, serializedObject.FindProperty(nameof(_target.argument)));
            eCustomEditors.DrawProperty(eGUI.yellowLt, serializedObject.FindProperty(nameof(_target.onResult)));
        }
    }
}
