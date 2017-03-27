using Smart.Custom;
using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Bindings.Values
{
    [CustomEditor(typeof(AudioBinding))]
    public class eAudioBinding : eEditor<AudioBinding>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawProperty(eGUI.crimsonLt, serializedObject.FindProperty(nameof(_target.updatesPerSecond)));
            eBinding.Draw(serializedObject, _target, eBinding.BuildFieldsAndProperties<AudioClip>);
            eCustomEditors.DrawProperty(eGUI.crimsonLt, serializedObject.FindProperty(nameof(_target.kind)));
            switch (_target.kind)
            {
                case AudioBinding.Kind.AudioSource:
                    eCustomEditors.DrawObjectProperty(serializedObject.FindProperty(nameof(_target.audioSource)));
                    break;
            }
        }
    }
}
