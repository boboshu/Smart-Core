using System;
using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Custom
{
    public class eStringDialog : EditorWindow
    {
        private string _text;
        private Action<string> _setText;

        private void OnGUI()
        {
            eGUI.RememberColors();
            GUILayout.BeginVertical(new GUIStyle { normal = { background = eIcons.Get("icons/statemachineeditor.background.png") } });
            {
                GUI.SetNextControlName("e");
                _text = EditorGUILayout.TextField(_text, GUI.skin.textField, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
                DrawBottomButtons();
            }
            GUILayout.EndVertical();
            eGUI.ResetColors();
            GUI.FocusControl("e");

            // Apply shortcut
            if (Event.current.keyCode == KeyCode.Return)
            {
                _setText(_text);
                Close();
            }
        }

        private void DrawBottomButtons()
        {
            eGUI.SetFGColor(Color.white);
            eGUI.SetBGColor(Color.black);

            // Draw tool buttons
            GUILayout.BeginHorizontal(GUI.skin.textArea);
            {
                GUILayout.FlexibleSpace();
                if (_setText == null)
                {
                    if (eGUI.Button("Close", eGUI.greenLt, 100, 24)) Close();
                }
                else
                {
                    if (eGUI.Button("Clear", eGUI.redLt, 100, 24)) { _setText(""); Close(); }
                    if (eGUI.Button("Cancel", eGUI.yellowLt, 100, 24)) Close();
                    if (eGUI.Button("Save", eGUI.greenLt, 100, 24)) { _setText(_text); Close(); }
                }
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
            eGUI.ResetColors();
        }


        public static void Execute(string text, Action<string> setText = null, bool wide = false)
        {
            var WIDTH = wide ? 800 : 400;
            var HEIGHT = 60;

            var w = GetWindow<eStringDialog>(true, "String Dialog");
            w.minSize = new Vector2(WIDTH, HEIGHT);
            w.position = new Rect((Screen.currentResolution.width - WIDTH) * 0.5f, (Screen.currentResolution.height - HEIGHT) * 0.5f, WIDTH, HEIGHT);
            w._text = text;
            w._setText = setText;
            w.Show();
        }
    }
}