using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Managers
{
    [CustomEditor(typeof(UpdatesManager))]
    public class eUpdatesManager : eEditor<UpdatesManager>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawBoolean(serializedObject.FindProperty(nameof(_target.dontDestroyOnLoad)));
        }
    }
}
