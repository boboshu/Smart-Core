using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(FloatEventHelper))]
    public class eFloatEventHelper : eEditor<FloatEventHelper>
    {
        protected override void DrawComponent()
        {
            if (_target.sourceMinimum >= _target.sourceMaximum) _target.sourceMinimum = _target.sourceMaximum - 1;
            if (_target.normalizedMinimum >= _target.normalizedMaximum) _target.normalizedMinimum = _target.normalizedMaximum - 1;

            eCustomEditors.DrawProperty(eGUI.redLt, serializedObject.FindProperty(nameof(_target.redirect)));
            eCustomEditors.DrawProperty(eGUI.magentaLt, serializedObject.FindProperty(nameof(_target.floatSource)));

            if (_target.floatSource >= FloatEventHelper.FloatSource.RotationX && _target.floatSource <= FloatEventHelper.FloatSource.RotationZ)
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.clampAngle180)));

            eCustomEditors.DrawProperty(eGUI.greenLt, serializedObject.FindProperty(nameof(_target.sourceMinimum)));
            eCustomEditors.DrawProperty(eGUI.greenLt, serializedObject.FindProperty(nameof(_target.sourceMaximum)));

            eCustomEditors.DrawProperty(eGUI.blueLt, serializedObject.FindProperty(nameof(_target.normalizedMinimum)));
            eCustomEditors.DrawProperty(eGUI.blueLt, serializedObject.FindProperty(nameof(_target.normalizedMaximum)));

            eCustomEditors.DrawProperty(eGUI.yellow, serializedObject.FindProperty(nameof(_target.onValueChanged)));
            eCustomEditors.DrawProperty(eGUI.yellow, serializedObject.FindProperty(nameof(_target.onValueMinimum)));
            eCustomEditors.DrawProperty(eGUI.yellow, serializedObject.FindProperty(nameof(_target.onValueMaximum)));
        }
    }
}