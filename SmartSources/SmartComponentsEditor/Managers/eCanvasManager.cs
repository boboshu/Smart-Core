using Smart.Custom;
using Smart.Editors;
using UnityEditor;

namespace Smart.Managers
{
    [CustomEditor(typeof(CanvasManager))]
    public class eCanvasManager : eEditor<CanvasManager>
    {
        protected override void DrawComponent()
        {
            eCustomEditors.DrawBoolean(serializedObject.FindProperty(nameof(_target.dontDestroyOnLoad)));

            eGUI.BeginColors();
            eGUI.SetColor(eGUI.violet);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.initialCanvas)));
            eGUI.EndColors();

            eGUI.EmptySpace();
            eGUI.EmptySpace();

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onOpen)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onOpenPopup)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.onClosePopup)));
        }
    }
}
