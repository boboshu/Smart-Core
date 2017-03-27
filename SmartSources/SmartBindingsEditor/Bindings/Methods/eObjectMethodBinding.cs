using Smart.Bindings.Method;
using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Bindings.Methods
{
    [CustomEditor(typeof(ObjectMethodBinding))]
    public class eObjectMethodBinding : eEditor<ObjectMethodBinding>
    {
        protected override void DrawComponent()
        {
            eBinding.Draw(serializedObject, _target, eBinding.BuildMethods<Object>);
        }
    }
}
