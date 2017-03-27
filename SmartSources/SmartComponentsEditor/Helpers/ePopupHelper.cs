using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(PopupHelper))]
    public class ePopupHelper : eEditorMany<PopupHelper>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawObjectProperty(serializedObject.FindProperty(nameof(_target.popupCanvas)));
        }
    }
}
