using Smart.Custom;
using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Helpers
{
    [CustomEditor(typeof(MovementHelper))]
    public class eMovementHelper : eEditor<MovementHelper>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.azureLt, serializedObject.FindProperty(nameof(_target.speed)));
            eGUI.CollectionToolbar(ref _target.presets, () => Vector3.zero, "Presets");
            eGUI.Collection(ref _target.presets, (v, i) =>
            {
                GUILayout.Label("#" + i + ":");
                eGUI.Vector3Editor(ref _target.presets[i]);
            }, eCollection.Delete_SingleLine_Reorder);
        }
    }
}