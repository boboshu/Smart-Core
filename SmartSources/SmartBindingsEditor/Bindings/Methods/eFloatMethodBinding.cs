using Smart.Bindings.Method;
using Smart.Editors;
using UnityEditor;

namespace Smart.Bindings.Methods
{
    [CustomEditor(typeof(FloatMethodBinding))]
    public class eFloatMethodBinding : eEditor<FloatMethodBinding>
    {
        protected override void DrawComponent()
        {
            eBinding.Draw(serializedObject, _target, eBinding.BuildMethods<float>);
        }
    }
}
