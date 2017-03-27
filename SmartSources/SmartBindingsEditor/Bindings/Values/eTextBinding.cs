using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Bindings.Values
{
    [CustomEditor(typeof(TextBinding))]
    public class eTextBinding : eEditor<TextBinding>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.crimsonLt, serializedObject.FindProperty(nameof(_target.updatesPerSecond)));
            eBinding.Draw(serializedObject, _target, eBinding.BuildFieldsAndProperties<string>);
            eCustomEditors.DrawProperty(eGUI.crimsonLt, serializedObject.FindProperty(nameof(_target.kind)));
            switch (_target.kind)
            {
                case TextBinding.Kind.Text:
                    eCustomEditors.DrawObjectProperty(serializedObject.FindProperty(nameof(_target.text)));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.format)));
                    break;

                case TextBinding.Kind.ResourceSprite:
                    eCustomEditors.DrawObjectProperty(serializedObject.FindProperty(nameof(_target.image)));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.folder)));
                    break;

                case TextBinding.Kind.Call:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.call)));
                    break;
            }
        }
    }
}
