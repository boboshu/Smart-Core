using UnityEditor;
using UnityEngine;

namespace Smart.Extensions
{
    public class eShortcutsDialog : EditorWindow
    {
        [MenuItem("Tools/Smart/Shortcuts Info _F1", false, 101)]
        private static void MainMenuItem()
        {
            if (_window) _window.Close();
            else CreateWindow();
        }

        public static void CreateWindow()
        {
            if (_image == null) _image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Smart/Extensions/Editor/Shortcuts.psd");
            _window = GetWindow<eShortcutsDialog>(true, "Smart Shortctuts");
            _window.minSize = new Vector2(_image.width, _image.height);
            _window.position = new Rect(100, 100, _image.width, _image.height);
            _window.Show();
        }

        private static eShortcutsDialog _window;
        private static Texture2D _image;

        private void OnGUI()
        {
            GUILayout.BeginVertical(new GUIStyle { normal = { background = _image } });
            {
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndVertical();
        }
   }
}