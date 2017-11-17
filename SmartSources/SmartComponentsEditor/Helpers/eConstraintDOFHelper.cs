using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Helpers
{
    [CustomEditor(typeof(ConstraintDOFHelper))]
    public class eConstraintDOFHelper : eEditorMany<ConstraintDOFHelper>
    {
        protected override void DrawComponent()
        {        
            eCustomEditors.DrawProperty(eGUI.redLt, serializedObject.FindProperty(nameof(_target.positionX)));
            if (_target.positionX == ConstraintDOFHelper.ConstraintType.Range)
            {
                eCustomEditors.DrawProperty(eGUI.redLt, serializedObject.FindProperty(nameof(_target.positionX_Min)));
                eCustomEditors.DrawProperty(eGUI.redLt, serializedObject.FindProperty(nameof(_target.positionX_Max)));
            }

            eCustomEditors.DrawProperty(eGUI.greenLt, serializedObject.FindProperty(nameof(_target.positionY)));
            if (_target.positionY == ConstraintDOFHelper.ConstraintType.Range)
            {
                eCustomEditors.DrawProperty(eGUI.greenLt, serializedObject.FindProperty(nameof(_target.positionY_Min)));
                eCustomEditors.DrawProperty(eGUI.greenLt, serializedObject.FindProperty(nameof(_target.positionY_Max)));
            }

            eCustomEditors.DrawProperty(eGUI.blueLt, serializedObject.FindProperty(nameof(_target.positionZ)));
            if (_target.positionZ == ConstraintDOFHelper.ConstraintType.Range)
            {
                eCustomEditors.DrawProperty(eGUI.blueLt, serializedObject.FindProperty(nameof(_target.positionZ_Min)));
                eCustomEditors.DrawProperty(eGUI.blueLt, serializedObject.FindProperty(nameof(_target.positionZ_Max)));
            }

            eGUI.EmptySpace();
            eGUI.LineDelimiter();
            eGUI.EmptySpace();

            eCustomEditors.DrawProperty(eGUI.redLt, serializedObject.FindProperty(nameof(_target.rotationX)));
            if (_target.rotationX == ConstraintDOFHelper.ConstraintType.Range)
            {
                eCustomEditors.DrawProperty(eGUI.redLt, serializedObject.FindProperty(nameof(_target.rotationX_Min)));
                eCustomEditors.DrawProperty(eGUI.redLt, serializedObject.FindProperty(nameof(_target.rotationX_Max)));
            }

            eCustomEditors.DrawProperty(eGUI.greenLt, serializedObject.FindProperty(nameof(_target.rotationY)));
            if (_target.rotationY == ConstraintDOFHelper.ConstraintType.Range)
            {
                eCustomEditors.DrawProperty(eGUI.greenLt, serializedObject.FindProperty(nameof(_target.rotationY_Min)));
                eCustomEditors.DrawProperty(eGUI.greenLt, serializedObject.FindProperty(nameof(_target.rotationY_Max)));
            }

            eCustomEditors.DrawProperty(eGUI.blueLt, serializedObject.FindProperty(nameof(_target.rotationZ)));
            if (_target.rotationZ == ConstraintDOFHelper.ConstraintType.Range)
            {
                eCustomEditors.DrawProperty(eGUI.blueLt, serializedObject.FindProperty(nameof(_target.rotationZ_Min)));
                eCustomEditors.DrawProperty(eGUI.blueLt, serializedObject.FindProperty(nameof(_target.rotationZ_Max)));                
            }

            if (_target.rotationX == ConstraintDOFHelper.ConstraintType.Range || _target.rotationY == ConstraintDOFHelper.ConstraintType.Range || _target.rotationZ == ConstraintDOFHelper.ConstraintType.Range)
            {
                eCustomEditors.DrawProperty(eGUI.yellowLt, serializedObject.FindProperty(nameof(_target.anglesRangeType)));
            }
        }
    }
}
