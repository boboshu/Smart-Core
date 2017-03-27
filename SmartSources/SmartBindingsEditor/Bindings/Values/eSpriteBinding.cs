using Smart.Custom;
using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Bindings.Values
{
    [CustomEditor(typeof(SpriteBinding))]
    public class eSpriteBinding : eEditor<SpriteBinding>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.crimsonLt, serializedObject.FindProperty(nameof(_target.updatesPerSecond)));
            eBinding.Draw(serializedObject, _target, eBinding.BuildFieldsAndProperties<Sprite>);
            eCustomEditors.DrawProperty(eGUI.crimsonLt, serializedObject.FindProperty(nameof(_target.kind)));
            switch (_target.kind)
            {
                case SpriteBinding.Kind.Image:
                    eCustomEditors.DrawObjectProperty(serializedObject.FindProperty(nameof(_target.image)));
                    break;
            }
        }
    }
}
