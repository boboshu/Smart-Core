using Smart.Bindings.Method;
using Smart.Editors;
using UnityEditor;

namespace Smart.Bindings.Methods
{
    [CustomEditor(typeof(VoidMethodBinding))]
    public class eVoidMethodBinding : eEditor<VoidMethodBinding>
    {
        protected override void DrawComponent()
        {
            eBinding.Draw(serializedObject, _target, eBinding.BuildMethodsVoid);
        }
    }
}
