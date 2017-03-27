using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Bindings.Values
{
    [CustomEditor(typeof(IntegerBinding))]
    public class eIntegerBinding : eEditor<IntegerBinding>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.crimsonLt, serializedObject.FindProperty(nameof(_target.updatesPerSecond)));
            eBinding.Draw(serializedObject, _target, eBinding.BuildFieldsAndProperties<int>);
            eCustomEditors.DrawProperty(eGUI.crimsonLt, serializedObject.FindProperty(nameof(_target.kind)));
            switch (_target.kind)
            {
                case IntegerBinding.Kind.Text:
                    eCustomEditors.DrawObjectProperty(serializedObject.FindProperty(nameof(_target.text)));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.hideZero)));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.format)));
                    break;

                case IntegerBinding.Kind.Couner:
                    eCustomEditors.DrawObjectsCollection(ref _target.counterGameObjects, serializedObject.FindProperty(nameof(_target.counterGameObjects)), "Counter GameObjects");
                    break;

                case IntegerBinding.Kind.Call:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.call)));
                    break;
            }
        }
    }
}
