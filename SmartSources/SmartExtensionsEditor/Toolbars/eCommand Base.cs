using System;
using System.IO;
using Smart.Editors;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Smart.Toolbars
{
    public partial class eCommand : ScriptableObject
    {
        public enum Kind { System, MainMenuCall, AddComponent, AddScript }
        public Kind kind;

        public enum SystemKind { DoNothing, SaveScene, SaveBackup }
        public SystemKind systemKind;

        public Texture2D icon;
        public string unityIcon = "";

        public string parameter = "";
        public MonoScript script;

        public void Execute()
        {
            switch (kind)
            {
                case Kind.System:
                    ExecuteSpecial();
                    break;

                case Kind.MainMenuCall:
                    EditorApplication.ExecuteMenuItem(parameter);
                    break;

                case Kind.AddComponent:
                    AddComponent(ComponentsDictionary[parameter]);
                    break;

                case Kind.AddScript:
                    if (script) AddComponent(script.GetClass());
                    break;
            }
        }

        private void ExecuteSpecial()
        {
            switch (systemKind)
            {
                case SystemKind.DoNothing:
                    break;

                case SystemKind.SaveScene:
                    if (EditorSceneManager.SaveOpenScenes())
                        Debug.Log("Scene saved [" + DateTime.Now.ToString("HH:mm:ss") + "]");
                    break;

                case SystemKind.SaveBackup:
                    if (SceneManager.sceneCount <= 0) return;

                    for (var i = 0; i < SceneManager.sceneCount; i++)
                    {
                        var scene = SceneManager.GetSceneAt(i);
                        var bkpName = scene.path.Replace(".unity", " " + DateTime.Now.ToString("MM-dd HH-mm-ss") + ".unity");
                        EditorSceneManager.SaveScene(scene, bkpName, true);
                        Debug.Log("Backup [" + bkpName + "]");
                    }

                    break;
            }
        }

        private static void AddComponent(Type t)
        {
            if (Selection.activeGameObject == null)
            {
                var go = new GameObject(t.Name);
                go.transform.parent = Selection.activeTransform;
                Undo.RegisterCreatedObjectUndo(go, "Create " + t.Name);
                Selection.activeGameObject = go;

                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                go.transform.localScale = Vector3.one;
            }
            Selection.activeGameObject.AddComponent(t);
        }

        public Texture2D GetIcon()
        {
            return string.IsNullOrEmpty(unityIcon) ? icon : eIcons.Get(unityIcon);
        }
    }
}
