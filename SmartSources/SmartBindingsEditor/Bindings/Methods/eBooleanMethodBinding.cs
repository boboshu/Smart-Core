using Smart.Bindings.Method;
using Smart.Editors;
using UnityEditor;

namespace Smart.Bindings.Methods
{
    [CustomEditor(typeof(BooleanMethodBinding))]
    public class eBooleanMethodBinding : eEditor<BooleanMethodBinding>
    {
        protected override void DrawComponent()
        {
            eBinding.Draw(serializedObject, _target, eBinding.BuildMethods<bool>);
        }
    }
}
