using Smart.Custom;
using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Bindings.Values
{
    [CustomEditor(typeof(ColorBinding))]
    public class eColorBinding : eEditor<ColorBinding>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.crimsonLt, serializedObject.FindProperty(nameof(_target.updatesPerSecond)));
            eBinding.Draw(serializedObject, _target, eBinding.BuildFieldsAndProperties<Color>);
            eCustomEditors.DrawProperty(eGUI.crimsonLt, serializedObject.FindProperty(nameof(_target.kind)));
            switch (_target.kind)
            {
                case ColorBinding.Kind.Graphic:
                    eCustomEditors.DrawObjectProperty(serializedObject.FindProperty(nameof(_target.graphic)));
                    break;
            }
        }
    }
}
