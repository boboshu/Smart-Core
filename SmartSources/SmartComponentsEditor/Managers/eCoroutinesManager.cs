using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Managers
{
    [CustomEditor(typeof(CoroutinesManager))]
    public class eCoroutinesManager : eEditor<CoroutinesManager>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawBoolean(serializedObject.FindProperty(nameof(_target.dontDestroyOnLoad)));
        }
    }
}
