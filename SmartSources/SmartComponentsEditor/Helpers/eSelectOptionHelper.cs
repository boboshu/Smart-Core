using Smart.Editors;
using Smart.Extensions;
using UnityEditor;
using UnityEngine.Events;

namespace Smart.Helpers
{
    [CustomEditor(typeof(SelectOptionHelper))]
    public class eSelectOptionHelper : eEditorMany<SelectOptionHelper>
    {
        protected override void DrawComponent()
        {
            eGUI.CollectionToolbar(ref _target.options, () => new UnityEvent(), "Options");
            eGUI.PropCollection(serializedObject.FindProperty(nameof(_target.options)), ref _target.options, (v, p, i) => EditorGUILayout.PropertyField(p), eCustomEventDrawer.IsCollapsed() ? eCollection.Delete_SingleLine_Reorder : eCollection.Delete_SingleLine_Reorder | eCollection.VerticalTools);
        }
    }
}
