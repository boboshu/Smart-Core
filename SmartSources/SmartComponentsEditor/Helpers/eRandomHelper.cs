using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(RandomHelper))]
    public class eRandomHelper : eEditorMany<RandomHelper>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.blueLt, serializedObject.FindProperty(nameof(_target.@from)));
            eCustomEditors.DrawProperty(eGUI.blueLt, serializedObject.FindProperty(nameof(_target.to)));
            eCustomEditors.DrawProperty(eGUI.yellowLt, serializedObject.FindProperty(nameof(_target.onResult)));
        }
    }
}