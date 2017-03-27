using System.Linq;
using Smart.Custom;
using Smart.Editors;
using Smart.Extensions;
using UnityEditor;
using UnityEngine;

namespace Smart.Toolbars
{
    public static class eCommandsMenuItems
    {
        [MenuItem("Assets/Create/Smart Command")]
        public static void NewCommand()
        {
            eScriptableObject.CreateAssetInPlace<eCommand>("Command");
        }
    }

    [CustomEditor(typeof(eCommand))]
    public class eCommandEditor : eEditorScObj<eCommand>
    {
        protected override void DrawScriptableObject()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical();
                {
                    if (eGUI.Button("Test Command", eGUI.yellow)) _target.Execute();
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.kind)), GUIContent.none);
                    switch (_target.kind)
                    {
                        case eCommand.Kind.System:
                            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.systemKind)), GUIContent.none);
                            if (Event.current.keyCode == KeyCode.Return)
                                EditorGUIUtility.systemCopyBuffer = _target.systemKind.ToString();
                            break;

                        case eCommand.Kind.MainMenuCall:
                            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.parameter)), GUIContent.none);
                            if (Event.current.keyCode == KeyCode.Return)
                                EditorGUIUtility.systemCopyBuffer = _target.parameter.Replace('/', '-');
                            break;

                        case eCommand.Kind.AddComponent:
                            GUI.SetNextControlName("p");
                            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.parameter)), GUIContent.none);
                            if (GUI.GetNameOfFocusedControl() == "p")
                            {
                                var lp = _target.parameter.ToLower();
                                var searchList = eCommand.ComponentNames.Where(x => x.ToLower().Contains(lp));

                                if (Event.current.keyCode == KeyCode.Return)
                                {
                                    var text = searchList.FirstOrDefault();
                                    if (!string.IsNullOrEmpty(text))
                                    {
                                        serializedObject.FindProperty(nameof(_target.parameter)).stringValue = text;
                                        EditorGUIUtility.systemCopyBuffer = text;
                                    }
                                }

                                // Draw Component complete list
                                foreach (var text in searchList.Take(10))
                                    if (eGUI.Button(text, eGUI.azureLt))
                                    {
                                        serializedObject.FindProperty(nameof(_target.parameter)).stringValue = text;
                                        EditorGUIUtility.systemCopyBuffer = text;
                                    }
                            }
                            break;

                        case eCommand.Kind.AddScript:
                            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.script)), GUIContent.none);
                            break;
                    }
                }
                GUILayout.EndVertical();

                GUILayout.BeginVertical(GUILayout.Width(64));
                {
                    var content = new GUIContent { image = _target.GetIcon() };
                    if (GUILayout.Button(content, GUILayout.Width(64), GUILayout.Height(64)))
                        eBuiltInIcons.Execute(s =>
                        {
                            serializedObject.FindProperty(nameof(_target.unityIcon)).stringValue = s;
                            serializedObject.FindProperty(nameof(_target.icon)).objectReferenceValue = null;
                            serializedObject.ApplyModifiedProperties();
                        });
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_target.icon)), GUIContent.none, GUILayout.Width(64));
                    if (_target.icon) serializedObject.FindProperty(nameof(_target.unityIcon)).stringValue = "";
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
        }
    }
}
