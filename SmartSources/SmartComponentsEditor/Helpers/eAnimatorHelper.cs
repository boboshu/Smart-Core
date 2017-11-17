using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(AnimatorHelper))]
    public class eAnimatorHelper : eEditorMany<AnimatorHelper>
    {
        protected override void DrawComponent()
        {            
            eCustomEditors.DrawProperty(eGUI.redLt, serializedObject.FindProperty(nameof(_target.redirect)));
            eCustomEditors.DrawProperty(eGUI.greenLt, serializedObject.FindProperty(nameof(_target.enumInChildren)));

            eCustomEditors.DrawProperty(eGUI.blueLt, serializedObject.FindProperty(nameof(_target.parameterName)));
        }
    }
}
