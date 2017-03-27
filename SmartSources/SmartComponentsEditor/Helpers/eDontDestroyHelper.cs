using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(DontDestroyHelper))]
    public class eDontDestroyHelper : eEditor<DontDestroyHelper>
    {
        protected override void DrawComponent()
        {
        }
    }
}
