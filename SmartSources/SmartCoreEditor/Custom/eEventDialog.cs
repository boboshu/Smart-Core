using System;
using Smart.Editors;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Smart.Custom
{
    public class eEventDialog : EditorWindow
    {
        private Vector2 _scrollPostion;
        private HackedMenuElement[] _hackedData;
        private string _searchText = "";
        private HackedMenuElement _firstElement;
        private Object _targetObject;

        public class HackedMenuElement
        {
            public string text;
            public string textWithoutComponent;
            public string searchText;
            public string component;
            public Action onClick;
            public bool isDynamic;
        }

        private void OnGUI()
        {
            eGUI.RememberColors();
            GUILayout.BeginVertical();
            {
                DrawTopTools();
                DrawMethodsList();
                GUILayout.FlexibleSpace();
                DrawBottomButtons();
            }
            GUILayout.EndVertical();
            eGUI.ResetColors();
        }

        private void DrawTopTools()
        {
            eGUI.SetFGColor(Color.white);
            eGUI.SetBGColor(Color.black);

            // Draw argument filter buttons
            GUILayout.BeginHorizontal(GUI.skin.textArea);
            {
                eGUI.SetBGColor(Color.white);
                GUILayout.FlexibleSpace();
                GUI.SetNextControlName("edit");
                eGUI.TextEditor(ref _searchText);
                if (eGUI.ButtonMini("", eIcons.Get("icons/d_winbtn_win_close.png"), eGUI.redLt)) _searchText = "";
                EditorGUI.FocusTextInControl("edit");
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
            eGUI.ResetColors();

            // Auto apply first element by Enter key
            if (Event.current.isKey && Event.current.keyCode == KeyCode.Return && _firstElement != null)
            {
                _firstElement.onClick();
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
                if (eGUI.Button("Close", eGUI.greenLt, 100, 24)) Close();
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
            eGUI.ResetColors();
        }

        private void DrawMethodsList()
        {
            var stl = new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleLeft };
            var grp = "";
            var b = false;
            _firstElement = null;

            _scrollPostion = GUILayout.BeginScrollView(_scrollPostion, GUILayout.ExpandWidth(true));
            {
                var s = _searchText.ToLower();
                eGUI.BeginColors();
                foreach (var d in _hackedData)
                {
                    if (!string.IsNullOrEmpty(s) && !d.searchText.Contains(s)) continue;
                    if (grp != d.component) { grp = d.component; b = !b; }

                    if (d.isDynamic) eGUI.SetBGColor(eGUI.azure);
                    else eGUI.SetBGColor(b ? Color.white : Color.gray);

                    var content = new GUIContent((d.isDynamic) ? d.textWithoutComponent + " [ Dynamic ]" : d.textWithoutComponent, d.component);
                    if (!string.IsNullOrEmpty(d.component) && _targetObject)
                    {
                        var go = (_targetObject is GameObject) ? ((GameObject) _targetObject) : ((Component) _targetObject).gameObject;
                        var target = (d.component == "GameObject") ? (Object) go : go.GetComponent(d.component);
                        var h = eHierarchyIcons.GetIconHandler(target);
                        if (h == null) content.text = d.component + '.' + content.text;
                        else content.image = h.Get(target);
                    }

                    if (GUILayout.Button(content, stl, GUILayout.Height(22)))
                    {
                        d.onClick();
                        Close();
                    }
                    if (_firstElement == null) _firstElement = d;
                }
                eGUI.EndColors();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndScrollView();
        }

        public static void Execute(HackedMenuElement[] hackedData, Object targetObject)
        {
            const int WIDTH = 300;
            const int HEIGHT = 600;

            // setup group and search text
            foreach (var d in hackedData)
            {
                var i = d.text.IndexOf("/"); // remove component
                d.textWithoutComponent = d.searchText = (i < 0) ? d.text : d.text.Substring(i + 1);
                d.component = (i < 0) ? "" : d.text.Remove(i);

                i = d.searchText.IndexOf(" ("); // remove parameters
                if (i > 0) d.searchText = d.searchText.Remove(i);

                i = d.searchText.IndexOf(" "); // remove property type
                if (i > 0) d.searchText = d.searchText.Substring(i + 1);

                d.searchText = d.searchText.ToLower();
                if (d.text == "No Function") d.searchText = "";
            }

            var w = GetWindow<eEventDialog>(true, "Event Dialog");
            w.minSize = new Vector2(WIDTH, HEIGHT);
            w.position = new Rect((Screen.currentResolution.width - WIDTH) * 0.5f, (Screen.currentResolution.height - HEIGHT) * 0.5f, WIDTH, HEIGHT);
            w.titleContent = new GUIContent("Search Event Dialog");
            w._hackedData = hackedData;
            w._targetObject = targetObject;
            w.Show();
            w.Focus();
        }
    }
}