using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Bindings.Values
{
    [CustomEditor(typeof(BooleanBinding))]
    public class eBooleanBinding : eEditor<BooleanBinding>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.crimsonLt, serializedObject.FindProperty(nameof(_target.updatesPerSecond)));
            eBinding.Draw(serializedObject, _target, eBinding.BuildFieldsAndProperties<bool>);
            eCustomEditors.DrawProperty(eGUI.crimsonLt, serializedObject.FindProperty(nameof(_target.kind)));
            switch (_target.kind)
            {
                case BooleanBinding.Kind.GameObjectActivator:
                case BooleanBinding.Kind.GameObjectActivatorInvert:
                    eCustomEditors.DrawObjectProperty(serializedObject.FindProperty(nameof(_target.activatorTarget)));
                    break;

                case BooleanBinding.Kind.Call:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.call)));
                    break;

                case BooleanBinding.Kind.TwoCalls:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.callPositive)));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.callNegative)));
                    break;
            }
        }
    }
}
