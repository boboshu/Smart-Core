using System;
using System.Collections.Generic;
using System.Linq;
using Smart.Custom;
using Smart.Editors;
using Smart.Extensions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Smart.Toolbars
{
    public abstract class eToolbar : EditorWindow
    {
        private static GUIStyle _itemStyle;
        private static GUIStyle _selectedItemStyle;

        private Object _selectedPrefab;
        private double _mouseDownTime;
        private double _nextRepaintTime;

        public bool useToolbarColor = true;
        [SerializeField] public List<Object> prefabs = new List<Object>();

        protected static T OpenWindow<T>(string title)
            where T : eToolbar
        {
            var w = GetWindow<T>();
            w.titleContent.image = eIcons.Get("d_sceneviewortho");
            w.titleContent.text = title;
            w.minSize = new Vector2(96, 36);
            w.Show();
            return w;
        }

        protected abstract Color GetColor();

        void OnInspectorUpdate()
        {
            if (EditorApplication.timeSinceStartup > _nextRepaintTime)
            {
                _nextRepaintTime = EditorApplication.timeSinceStartup + 1;
                Repaint();
            }
        }

        void OnGUI()
        {
            eGUI.RememberColors();
            if (prefabs.RemoveAll(x => x == null) > 0) _selectedPrefab = null; // Clean up from empty links
            var evnt = Event.current;
            if (ProcessDrop(evnt)) return;
            DrawItems(evnt);
        }

        private void StartDrag()
        {
            if (!_selectedPrefab) return;
            DragAndDrop.PrepareStartDrag();
            DragAndDrop.paths = new[] { AssetDatabase.GetAssetPath(_selectedPrefab) };
            DragAndDrop.objectReferences = new[] { _selectedPrefab };
            DragAndDrop.StartDrag(_selectedPrefab.ToString());
            _mouseDownTime = EditorApplication.timeSinceStartup;
        }

        private void ContextMenu()
        {           
            var menu = new GenericMenu();
            if (_selectedPrefab)
            {
                menu.AddItem(new GUIContent("Open Item"), false, () => AssetDatabase.OpenAsset(_selectedPrefab));
                menu.AddItem(new GUIContent("Delete Item"), false, () => { prefabs.Remove(_selectedPrefab); _selectedPrefab = null; });
                menu.AddSeparator("");
            }
            menu.AddItem(new GUIContent("Toolbar Rename"), false, () => eStringDialog.Execute(titleContent.text, s => titleContent.text = s));
            menu.AddItem(new GUIContent("Toolbar Icon"), false, () => eBuiltInIcons.Execute(s => titleContent.image = eIcons.Get(s)));           
            menu.AddItem(new GUIContent("Toolbar Color"), useToolbarColor, () => useToolbarColor =! useToolbarColor);
            menu.AddItem(new GUIContent("Toolbar Remove All Items"), false, () =>
            {
                if (EditorUtility.DisplayDialog("Are you sure?", "Do you want to clear all items?", "Yes", "No")) prefabs.Clear();
            });
            menu.ShowAsContext();
        }

        private void DrawItems(Event evnt)
        {
            var itemClicked = false;
            if (_itemStyle == null) _itemStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            if (_selectedItemStyle == null) _selectedItemStyle = new GUIStyle(GUI.skin.box) { alignment = TextAnchor.MiddleCenter };

            if (useToolbarColor)
            {
                eGUI.SetBGColor(GetColor(), 0.3f);
                GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.textArea) { margin = new RectOffset(0, 0, 0, 0), padding = new RectOffset(0, 0, 0, 0) });
            }
            else
            {
                GUILayout.BeginHorizontal();    
            }           
            {
                if (prefabs.Any())
                {
                    foreach (var p in prefabs)
                    {
                        // Get image
                        var cmd = p as eCommand;
                        var content = new GUIContent
                        {
                            image = (cmd ? cmd.GetIcon() : AssetPreview.GetAssetPreview(p)) ?? AssetDatabase.GetCachedIcon(AssetDatabase.GetAssetPath(p)),
                            tooltip = p.name
                        };

                        GUILayout.Label(content, (_selectedPrefab == p) ? _selectedItemStyle : _itemStyle, GUILayout.Width(ItemSize), GUILayout.Height(ItemSize));

                        // Process mouse
                        if (evnt.type == EventType.MouseDown && GUILayoutUtility.GetLastRect().Contains(evnt.mousePosition))
                        {
                            _selectedPrefab = p;
                            if (evnt.button == 0) StartDrag(); // Left Click
                            itemClicked = true;
                        }

                    }
                    GUILayout.FlexibleSpace();
                }
                else
                {
                    GUILayout.Label("Drag & Drop items here from Project window", new GUIStyle(GUI.skin.label) { stretchWidth = true, stretchHeight = true, alignment = TextAnchor.MiddleCenter } );
                }
            }
            GUILayout.EndHorizontal();

            if (evnt.type == EventType.MouseDown)
            {
                if (!itemClicked) _selectedPrefab = null; // Click in nowhere
                if (evnt.button == 1) ContextMenu(); // Right Click
            }
        }

        private int ItemSize => Math.Min((int)position.height, 64);

        private bool ProcessDrop(Event evnt)
        {
            var processed = false;
            if (evnt.mousePosition.x > 0f && evnt.mousePosition.x < position.width && evnt.mousePosition.y > 0f && evnt.mousePosition.y < position.height)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Link;
                if (evnt.rawType == EventType.DragPerform)
                {
                    var dropIndex = (int)evnt.mousePosition.x / (ItemSize + 4);
                    foreach (var obj in DragAndDrop.objectReferences)
                    {
                        if (prefabs.Contains(obj) || string.IsNullOrEmpty(AssetDatabase.GetAssetPath(obj))) // object can`t be added
                        {
                            if (prefabs.IndexOf(obj) == dropIndex) // itself
                            {
                                if (EditorApplication.timeSinceStartup - _mouseDownTime < 0.5) // Execute item command
                                {
                                    var cmd = _selectedPrefab as eCommand;
                                    if (cmd) cmd.Execute();
                                    else AssetDatabase.OpenAsset(_selectedPrefab);
                                }
                                else
                                {
                                    Selection.activeObject = _selectedPrefab;
                                }
                            }
                            else // move this object on toolbar
                            {
                                Selection.activeObject = _selectedPrefab;
                                prefabs.Remove(obj);
                                AddPrefab(dropIndex, obj);
                            }
                        }
                        else
                        {
                            AddPrefab(dropIndex, obj);
                        }
                    }
                    processed = true;
                    _selectedPrefab = null;
                }
            }
            return processed;
        }

        private void AddPrefab(int dropIndex, Object obj)
        {
            if (dropIndex < prefabs.Count) prefabs.Insert(dropIndex, obj);
            else prefabs.Add(obj);
        }
    }
}