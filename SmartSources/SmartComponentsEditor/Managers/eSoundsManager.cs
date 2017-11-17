using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Managers
{
    [CustomEditor(typeof(SoundsManager))]
    public class eSoundsManager : eEditor<SoundsManager>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawBoolean(serializedObject.FindProperty(nameof(_target.dontDestroyOnLoad)));
        }
    }
}
