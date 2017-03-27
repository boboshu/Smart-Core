using Smart.Bindings.Method;
using Smart.Editors;
using UnityEditor;

namespace Smart.Bindings.Methods
{
    [CustomEditor(typeof(TextMethodBinding))]
    public class eTextMethodBinding : eEditor<TextMethodBinding>
    {
        protected override void DrawComponent()
        {
            eBinding.Draw(serializedObject, _target, eBinding.BuildMethods<string>);
        }
    }
}
