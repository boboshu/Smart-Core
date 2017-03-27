using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Managers
{
    [CustomEditor(typeof(BindingsManager))]
    public class eBindingsManager : eEditor<BindingsManager>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawBoolean(serializedObject.FindProperty(nameof(_target.dontDestroyOnLoad)));
        }
    }
}
