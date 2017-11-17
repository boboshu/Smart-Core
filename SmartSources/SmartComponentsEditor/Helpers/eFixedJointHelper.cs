using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(FixedJointHelper))]
    public class eFixedJointHelper : eEditorMany<FixedJointHelper>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.redLt, serializedObject.FindProperty(nameof(_target.redirect)));
            eCustomEditors.DrawProperty(eGUI.greenLt, serializedObject.FindProperty(nameof(_target.enumInChildren)));
        }
    }
}