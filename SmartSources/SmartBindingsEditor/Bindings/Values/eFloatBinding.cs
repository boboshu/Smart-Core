using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Bindings.Values
{
    [CustomEditor(typeof(FloatBinding))]
    public class eFloatBinding : eEditor<FloatBinding>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.crimsonLt, serializedObject.FindProperty(nameof(_target.updatesPerSecond)));
            eBinding.Draw(serializedObject, _target, eBinding.BuildFieldsAndProperties<float>);
            eCustomEditors.DrawProperty(eGUI.crimsonLt, serializedObject.FindProperty(nameof(_target.kind)));
            switch (_target.kind)
            {
                case FloatBinding.Kind.Text:
                    eCustomEditors.DrawObjectProperty(serializedObject.FindProperty(nameof(_target.text)));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.hideZero)));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.format)));
                    break;

                case FloatBinding.Kind.Couner:
                    eCustomEditors.DrawObjectsCollection(ref _target.counterGameObjects, serializedObject.FindProperty(nameof(_target.counterGameObjects)), "Counter GameObjects");
                    break;

                case FloatBinding.Kind.Call:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.call)));
                    break;
            }
        }
    }
}
