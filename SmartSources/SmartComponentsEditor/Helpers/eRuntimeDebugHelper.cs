using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(RuntimeDebugHelper))]
    public class eRuntimeDebugHelper : eEditor<RuntimeDebugHelper>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.yellowLt, serializedObject.FindProperty(nameof(_target.onExecute)));
            if (eGUI.Button("Execute", eGUI.redLt)) _target.onExecute.Invoke();
        }
    }
}