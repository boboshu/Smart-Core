using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(RotationHelper))]
    public class eRotationHelper : eEditor<RotationHelper>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.azureLt, serializedObject.FindProperty(nameof(_target.speed)));            
        }
    }
}