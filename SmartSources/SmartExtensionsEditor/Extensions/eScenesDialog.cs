using System.IO;
using System.Linq;
using Smart.Editors;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Smart.Extensions
{
    internal class eScenesDialog : EditorWindow
    {
        private Vector2 _scrollPostion;

        private void OnGUI()
        {
            eGUI.RememberColors();
            GUILayout.BeginVertical(new GUIStyle { normal = { background = eIcons.Get("icons/statemachineeditor.background.png") } });
            {
                GUILayout.BeginHorizontal();
                {
                    DrawScenes();
                }
                GUILayout.EndHorizontal();
                DrawBottomButtons();
            }
            GUILayout.EndVertical();
            eGUI.ResetColors();
        }

        private void OnDestroy()
        {
            _window = null;
            PlayerPrefs.SetInt("dialogLeft", (int)position.xMin);
            PlayerPrefs.SetInt("dialogTop", (int)position.yMin);
            PlayerPrefs.SetInt("dialogWidth", (int)position.width);
            PlayerPrefs.SetInt("dialogHeight", (int)position.height);
            PlayerPrefs.Save();
        }

        private void DrawBottomButtons()
        {
            eGUI.SetFGColor(Color.white);
            eGUI.SetBGColor(Color.black);

            eGUI.EmptySpace();
            GUILayout.BeginHorizontal(GUI.skin.textArea);
            {
                GUILayout.FlexibleSpace();
                if (eGUI.Button("Done", eGUI.greenLt, 100, 24)) Close();
            }
            GUILayout.EndHorizontal();
        }

        private void DrawScenes()
        {
            eGUI.SetFGColor(Color.white);
            eGUI.SetBGColor(Color.black);
            GUILayout.BeginVertical(GUI.skin.textArea);
            {
                _scrollPostion = GUILayout.BeginScrollView(_scrollPostion);
                {
                    var path = Application.dataPath + Path.DirectorySeparatorChar;
                    foreach (var scene in _scenes)
                    {
                        var stl = new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleLeft };
                        var scenePath = scene.Replace(path, "");
                        var sceneName = scenePath.Replace(".unity", "");
                        var inBuild = _buildScenes.Contains(scenePath);
                        if (inBuild) { eGUI.BeginColors(); eGUI.SetColor(eGUI.azure); }
                        if (GUILayout.Button(sceneName, stl, GUILayout.ExpandWidth(true)))
                        {
                            Close();
                            //EditorApplication.OpenScene(scene); fix unity 5.5 warn
                            EditorSceneManager.OpenScene(scene);
                        }
                        if (inBuild) eGUI.EndColors();
                    }
                }
                GUILayout.EndScrollView();

                GUILayout.FlexibleSpace();
            }
            GUILayout.EndVertical();
        }

       
        private static eScenesDialog _window;

        public static void Toggle()
        {
            if (_window) _window.Close();
            else Execute();
        }

        public static void Execute()
        {
            const int MIN_WIDTH = 700;
            const int MIN_HEIGHT = 500;

            var width = PlayerPrefs.GetInt("dialogWidth", MIN_WIDTH);
            var height = PlayerPrefs.GetInt("dialogHeight", MIN_HEIGHT);
            var left = PlayerPrefs.GetInt("dialogLeft", (Screen.currentResolution.width - width) / 2);
            var top = PlayerPrefs.GetInt("dialogTop", (Screen.currentResolution.height - height) / 2);

            _window = GetWindow<eScenesDialog>(true, "Smart Scenes");
            _window.minSize = new Vector2(MIN_WIDTH, MIN_HEIGHT);
            _window.position = new Rect(left, top, width, height);
            _window._scenes = Directory.GetFiles(Application.dataPath, "*.unity", SearchOption.AllDirectories);
            _window._buildScenes = EditorBuildSettings.scenes.Select(s => s.path.Replace("Assets/", "")).ToArray();
            _window.Show();
        }

        private string[] _scenes;
        private string[] _buildScenes;
    }
}