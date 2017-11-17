using System;
using System.Linq;
using Smart.Extensions;
using Smart.Types;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Smart.Editors
{
    [CanEditMultipleObjects]
    public abstract class eEditor<T> : Editor
        where T : MonoBehaviour
    {
        protected T _target;
        private bool _showControlsToolbar;

        public override void OnInspectorGUI()
        {
            _target = target as T;
            if (_target == null) return;
            serializedObject.Update();

            if (typeof(T).GetCustomAttributes(typeof(FirstInListAttribute), true).Any()) MakeFirstComponent();
            if (typeof(T).GetCustomAttributes(typeof(ClearTransformAttribute), true).Any()) ClearTransform();

            // show controls toolbar
            var oldShowToolbar = _showControlsToolbar;
            _showControlsToolbar = Event.current.control && Event.current.alt;
            if (oldShowToolbar != _showControlsToolbar) Repaint();
            if (_showControlsToolbar) { DrawControlsToolbar(); return; }

            // Main draw
            if (PrefabUtility.GetPrefabType(_target.gameObject) == PrefabType.Prefab)
            {
                ProcessDraw();
            }
            else
            {
                var p = GetAllowedParentType();
                if (p == null) // don`t care about parent
                {
                    ProcessDraw();
                }
                else if (p == typeof(object))
                {
                    if (_target.transform.parent == null) ProcessDraw();
                    else ProcessError("Requires to be placed into the root of Hierarchy!");
                }
                else if (_target.transform.parent == null)
                {
                    ProcessError("Requires parent! (with \"" + p.Name + "\" component)");
                }
                else if (_target.transform.parent.gameObject.GetComponent(p) == null)
                {
                    ProcessError("Requires parent with \"" + p.Name + "\" component!");
                }
                else ProcessDraw();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void ClearTransform()
        {
            _target.transform.localPosition = Vector3.zero;
            _target.transform.localRotation = Quaternion.identity;
            _target.transform.localScale = Vector3.one;
        }

        private void ProcessError(string msg)
        {
            eGUI.InspectorHeader(msg, true);

            var s = "";
            var t = _target.transform;
            while (t != null)
            {
                s = string.IsNullOrEmpty(s) ? t.name : t.name + ">" + s;
                t = t.parent;
            }

            Debug.LogError("\"" + s + "\"\n" + msg);
        }

        private void ProcessDraw()
        {
            var err = OnValidate();
            if (IsOnlyOne() && _target.GetComponents<T>().Count() > 1)
                err = "Only one copy of this component per GameObject is allowed.";
            if (err != null) ProcessError(err);
            eGUI.RememberColors();
            DrawComponent();
            eGUI.ResetColors();
        }

        /// <summary>
        /// Move inspected component above all other components just after transform
        /// </summary>
        private void MakeFirstComponent()
        {
            var t = typeof(T);
            foreach (var cmp in _target.GetComponents<Component>())
            {
                if (cmp == _target) break; // break when found itself
                if (cmp is Transform) continue; // ignore Transform
                var tp = cmp.GetType();
                if (tp == t) continue; // ignore components of same type
                if (tp.GetCustomAttributes(typeof(FirstInListAttribute), true).Any()) break; // found some same attribute
                ComponentUtility.MoveComponentUp(_target);
            }
        }

        protected abstract void DrawComponent();

        // return NULL to allow any parent
        // return typeof(object) to force a null parent (root only)
        // return typeof(Component) to restrict parent type
        protected virtual Type GetAllowedParentType()
        {
            return null;
        }

        protected virtual string OnValidate()
        {
            return null;
        }

        protected virtual bool IsOnlyOne()
        {
            return true;
        }

        protected bool IsPrefab()
        {
            return PrefabUtility.GetPrefabType(_target.gameObject) == PrefabType.Prefab;
        }

        protected void NewButton<MB>(bool asChild = true) where MB : MonoBehaviour
        {
            var n = typeof(MB).Name;
            n = n.Substring(1, n.Length > 9 ? 8 : n.Length - 1);

            var tt = eHierarchyIcons.GetIconHandler<MB>();
            if (!GUILayout.Button(new GUIContent("New\n" + n, tt == null ? null : tt.Get()), GUILayout.Width(84), GUILayout.Height(32))) return;

            if (asChild)
            {
                var go = new GameObject(n);
                go.transform.SetParent(_target.transform, false);
                go.AddComponent<MB>();
                Undo.RegisterCreatedObjectUndo(go, "Create New " + n);
                eUtils.HierarchyFoldAll(_target.gameObject, true);
            }
            else
            {
                _target.gameObject.AddComponent<MB>();
            }
        }

        private void DrawControlsToolbar()
        {
            EditorGUILayout.BeginHorizontal();
            {
                if (eGUI.ButtonMini("▼", "Move Down", eGUI.greenLt, 30)) ComponentUtility.MoveComponentDown(_target);
                if (eGUI.ButtonMini("▲", "Move Up", eGUI.greenLt, 30)) ComponentUtility.MoveComponentUp(_target);

                GUILayout.FlexibleSpace();

                if (eGUI.ButtonMini("", eIcons.Get("icons/clipboard.png"), "Copy Component", eGUI.azureLt, 30)) ComponentUtility.CopyComponent(_target);
                if (eGUI.ButtonMini("", eIcons.Get("icons/vcs_document.png"), "Paste Component", eGUI.azureLt, 30)) ComponentUtility.PasteComponentValues(_target);
                if (eGUI.ButtonMini("", eIcons.Get("icons/vcs_document.png"), "Past New Component After This", eGUI.greenLt, 30) && ComponentUtility.PasteComponentAsNew(_target.gameObject))
                {
                    var components = _target.GetComponents<Component>();
                    var newComponent = components.Last();
                    var cnt = components.Length - components.IndexOf(_target) - 2;
                    while (cnt-- > 0)
                        ComponentUtility.MoveComponentUp(newComponent);
                }

                GUILayout.FlexibleSpace();

                if (eGUI.ButtonMini("", eIcons.Get("icons/d_p4_deletedlocal.png"), "Remove Component", eGUI.crimsonLt, 30)) DestroyImmediate(_target);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    [CanEditMultipleObjects]
    public abstract class eEditorMany<T> : eEditor<T>
        where T : MonoBehaviour
    {
        protected sealed override bool IsOnlyOne()
        {
            return false;
        }
    }

    [CanEditMultipleObjects]
    public abstract class eEditorScObj<T> : Editor
        where T : ScriptableObject
    {
        protected T _target;

        public override void OnInspectorGUI()
        {
            _target = target as T;
            if (_target == null) return;
            serializedObject.Update();

            eGUI.RememberColors();
            DrawScriptableObject();
            eGUI.ResetColors();

            serializedObject.ApplyModifiedProperties();
        }

        protected abstract void DrawScriptableObject();
    }
}