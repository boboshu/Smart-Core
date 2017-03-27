using Smart.Bindings.Method;
using Smart.Editors;
using UnityEditor;

namespace Smart.Bindings.Methods
{
    [CustomEditor(typeof(IntegerMethodBinding))]
    public class eIntegerMethodBinding : eEditor<IntegerMethodBinding>
    {
        protected override void DrawComponent()
        {
            eBinding.Draw(serializedObject, _target, eBinding.BuildMethods<int>);
        }
    }
}
