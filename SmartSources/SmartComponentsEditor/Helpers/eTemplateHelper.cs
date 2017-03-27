using Smart.Custom;
using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Helpers
{
    [CustomEditor(typeof(TemplateHelper))]
    public class eTemplateHelper : eEditor<TemplateHelper>
    {
        protected override void DrawComponent()
        {
            if (_target.template)
            {
                if (PrefabUtility.GetPrefabType(_target.template) != PrefabType.Prefab) EditorGUILayout.HelpBox("You should use prefab", MessageType.Error);
                if (_target.template == _target.gameObject) EditorGUILayout.HelpBox("You can`t use self object as template", MessageType.Error);
            }
            eCustomEditors.DrawObjectProperty(serializedObject.FindProperty(nameof(_target.template)));
            eCustomEditors.DrawProperty(eGUI.greenLt, serializedObject.FindProperty(nameof(_target.mode)));
            if (_target.mode != TemplateHelper.Mode.InstantiateGameObject)
            {
                EditorGUILayout.HelpBox("This mode doesn`t redirect UnityEvents\nof source components to a new objects!", MessageType.Warning);
                eCustomEditors.DrawProperty(eGUI.greenLt, serializedObject.FindProperty(nameof(_target.syncExistingComponents)));
                eGUI.EmptySpace();
            }

            eGUI.CollectionToolbar(ref _target.redirectCommandsTo, () => null, "Redirect Commands To");
            eGUI.PropCollection(serializedObject.FindProperty(nameof(_target.redirectCommandsTo)), ref _target.redirectCommandsTo, (v, p, i) => EditorGUILayout.PropertyField(p, GUIContent.none), eCollection.Delete_SingleLine_Reorder);
        }
    }
}