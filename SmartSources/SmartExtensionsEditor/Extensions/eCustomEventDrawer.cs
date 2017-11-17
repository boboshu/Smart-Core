using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Smart.Custom;
using Smart.Editors;
using UnityEditor;
using UnityEditor.Events;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Smart.Extensions
{
    [CustomPropertyDrawer(typeof(UnityEvent), true)]
    [CustomPropertyDrawer(typeof(UnityEvent<string>), true)]
    [CustomPropertyDrawer(typeof(UnityEvent<int>), true)]
    [CustomPropertyDrawer(typeof(UnityEvent<float>), true)]
    [CustomPropertyDrawer(typeof(UnityEvent<bool>), true)]
    [CustomPropertyDrawer(typeof(UnityEvent<GameObject>), true)]
    [CustomPropertyDrawer(typeof(UnityEvent<Sprite>), true)]
    [CustomPropertyDrawer(typeof(UnityEvent<AudioClip>), true)]
    [CustomPropertyDrawer(typeof(UnityEvent<Color>), true)]
    [CustomPropertyDrawer(typeof(UnityEvent<Transform>), true)]
    [CustomPropertyDrawer(typeof(UnityEvent<Object>), true)]
    [CustomPropertyDrawer(typeof(UnityEvent<Vector2>), true)]
    [CustomPropertyDrawer(typeof(UnityEvent<Vector3>), true)]
    [CustomPropertyDrawer(typeof(UnityEvent<Texture2D>), true)]
    [CustomPropertyDrawer(typeof(UnityEvent<Material>), true)]
    public class eCustomEventDrawer : UnityEventDrawer
    {
        private Object _eventOwner;
        private UnityEventBase _evnt;
        private SerializedProperty _property;
        private int _runtimeCount;

        private enum Mode { Browse, Reorder, Delete }
        private static Mode _mode;


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (eSettings.EventsExtension)
            {                
                SetupInternalValues(property);

                eGUI.BeginColors();
                eGUI.SetColor(eGUI.yellowLt);

                if (IsCollapsed())
                {
                    OnInspectorGUI_Collapsed(position);
                }
                else
                {
                    base.OnGUI(position, property, label);
                    DrawExtrasAndDoAutoFill(position);
                    EditorApplication.RepaintAnimationWindow();
                }
                eGUI.EndColors();
            }
            else
            {
                base.OnGUI(position, property, label);
            }
        }

        public static bool IsCollapsed()
        {
            return EditorApplication.timeSinceStartup > _forceExpandTime;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return IsCollapsed() ? 16 : base.GetPropertyHeight(property, label);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------

        private static double _forceExpandTime;
        private static GUIStyle _collapsedStyle;

        public void OnInspectorGUI_Collapsed(Rect position)
        {
            if (_collapsedStyle == null)
                _collapsedStyle = new GUIStyle(GUI.skin.textField)
                {
                    normal = { textColor = Color.gray },
                    padding = new RectOffset(1, 1, 1, 1),
                    overflow = new RectOffset(),
                    margin = new RectOffset(1, 1, 1, 1),
                    alignment = TextAnchor.MiddleCenter,
                    stretchWidth = true,
                    fontStyle = FontStyle.Bold,                    
                };

            if (GUI.Button(position, ObjectNames.NicifyVariableName(_property.name) + " [" + (_evnt.GetPersistentEventCount() + _runtimeCount) + "]", _collapsedStyle))
                _forceExpandTime = EditorApplication.timeSinceStartup + 300; // seconds of collapsing not allowed
        }

        //----------------------------------------------------------------------------------------------------------------------------------------

        private void SetupInternalValues(SerializedProperty property)
        {
            if (_eventOwner && _property == property) return;
            _eventOwner = property.serializedObject.targetObject;
            _evnt = GetFieldValueByPath(_eventOwner, property.propertyPath.Replace(".Array.data[", "[")) as UnityEventBase;
            _property = property;
            _runtimeCount = (int)_evnt.Reflection_FieldGet("m_Calls")?.Reflection_FieldGet("m_RuntimeCalls")?.Reflection_PropertyGet("Count");
        }

        private static object GetFieldValueByPath(object obj, string path)
        {
            var currentType = obj.GetType();

            foreach (var fn in path.Split('.'))
            {
                var i = fn.IndexOf("[");
                if (i > 0) // is indexed field
                {
                    var fieldName = fn;
                    fieldName = fieldName.Replace("]", "");
                    var indx = int.Parse(fieldName.Substring(i + 1));
                    fieldName = fieldName.Remove(i);
                    obj = GetFieldValue(obj, fieldName, currentType);
                    obj = (obj as object[])[indx];
                    currentType = obj.GetType();
                }
                else
                {
                    obj = GetFieldValue(obj, fn, currentType);
                    currentType = obj.GetType();
                }
            }
            return obj;
        }

        private static object GetFieldValue(object inst, string name, Type type = null)
        {
            if (type == null) type = inst.GetType();

            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                if (field.Name == name) return field.GetValue(inst);

            var baseType = type.BaseType;
            return baseType == null ? null : GetFieldValue(inst, name, baseType);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------

        public void DrawExtrasAndDoAutoFill(Rect r)
        {            
            // process auto fill
            for (var i = 0; i < _evnt.GetPersistentEventCount(); i++)
            {
                var target = _evnt.GetPersistentTarget(i);
                if (target) continue;
                if (_eventOwner is Component)
                {
                    UnityEventTools.RegisterStringPersistentListener(_evnt, i, (_eventOwner as Component).SendMessage, "");
                    ResetMethodInProperty(i);
                }
            }

            var stl = new GUIStyle(GUI.skin.button) { padding = new RectOffset(), margin = new RectOffset(), overflow = new RectOffset(), normal = GUI.skin.label.normal };

            // Draw Extra buttons
            var pec = _evnt.GetPersistentEventCount();
            for (var i = 0; i < pec; i++)
            {
                switch (_mode)
                {
                    case Mode.Browse:
                        if (GUI.Button(new Rect(r.xMin - 14, r.yMin + i*43 + 25, 20, 33), eIcons.Get("icons/d_viewtoolzoom on.png"), stl))
                            eEventDialog.Execute(BuildHackedData(i), _evnt.GetPersistentTarget(i));
                        break;

                    case Mode.Reorder:
                        if (i > 0 && GUI.Button(new Rect(r.xMin - 14, r.yMin + i * 43 + 20, 20, 20), "▲", stl)) MoveUpHack(i);
                        if (i < pec - 1 && GUI.Button(new Rect(r.xMin - 14, r.yMin + i * 43 + 40, 20, 20), "▼", stl)) MoveDownHack(i);
                        break;

                    case Mode.Delete:
                        if (GUI.Button(new Rect(r.xMin - 14, r.yMin + i * 43 + 25, 20, 33), eIcons.Get("icons/d_winbtn_win_close.png"), stl))
                            UnityEventTools.RemovePersistentListener(_evnt, i);
                        break;
                }
            }

            // draw Clear button            
            if (GUI.Button(new Rect(r.xMax - 19, r.yMin + 1, 18, 16), eIcons.Get("icons/d_winbtn_win_close.png"), stl))
                while (_evnt.GetPersistentEventCount() > 0)
                    UnityEventTools.RemovePersistentListener(_evnt, 0);

            // draw Toolbar buttons            
            eGUI.SetFGColor(new Color(1, 1, 1, 0), 0.9f);

            if (_mode == Mode.Browse) eGUI.ResetColors();
            if (GUI.Button(new Rect(r.center.x - 39, r.yMax - 17, 26, 18), eIcons.Get("icons/d_viewtoolzoom on.png"), stl)) _mode = Mode.Browse;
            if (_mode == Mode.Browse) eGUI.SetFGColor(new Color(1, 1, 1, 0), 0.9f);

            if (_mode == Mode.Reorder) eGUI.ResetColors();
            if (GUI.Button(new Rect(r.center.x - 13, r.yMax - 17, 26, 18), eIcons.Get("icons/d_rotatetool on.png"), stl)) _mode = Mode.Reorder;
            if (_mode == Mode.Reorder) eGUI.SetFGColor(new Color(1, 1, 1, 0), 0.9f);

            if (_mode == Mode.Delete) eGUI.ResetColors();
            if (GUI.Button(new Rect(r.center.x + 13, r.yMax - 17, 26, 18), eIcons.Get("d_treeeditor.trash"), stl)) _mode = Mode.Delete;

            // draw Runtime count
            if (_runtimeCount > 0)
            {
                eGUI.SetFGColor(new Color(1, 0.3f, 0.3f), 0.9f);
                GUI.Label(new Rect(r.xMin, r.yMax - 16, 96, 16), "Runtime: " + _runtimeCount, EditorStyles.boldLabel);
            }            
        }

        //----------------------------------------------------------------------------------------------------------------------------------------

        private void MoveUpHack(int indx)
        {
            _property.serializedObject.Update();
            _property.FindPropertyRelative("m_PersistentCalls.m_Calls").MoveArrayElement(indx, indx - 1);
            _property.serializedObject.ApplyModifiedProperties();
        }

        private void MoveDownHack(int indx)
        {
            _property.serializedObject.Update();
            _property.FindPropertyRelative("m_PersistentCalls.m_Calls").MoveArrayElement(indx, indx + 1);
            _property.serializedObject.ApplyModifiedProperties();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------

        private eEventDialog.HackedMenuElement[] BuildHackedData(int indx)
        {
            var list = new List<eEventDialog.HackedMenuElement>();

            var p1 = _property.FindPropertyRelative("m_PersistentCalls.m_Calls").GetArrayElementAtIndex(indx).FindPropertyRelative("m_Target").objectReferenceValue;
            var p2 = typeof(UnityEventDrawer).GetMethod("GetDummyEvent", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new[] { _property });
            var p3 = _property.FindPropertyRelative("m_PersistentCalls.m_Calls").GetArrayElementAtIndex(indx);
            var buildPopupList = typeof(UnityEventDrawer).GetMethod("BuildPopupList", BindingFlags.Static | BindingFlags.NonPublic);
            var gm = buildPopupList.Invoke(null, new[] { p1, p2, p3 }) as GenericMenu;

            var isDynamic = false;
            var menuItems = gm.GetType().GetField("menuItems", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(gm) as ArrayList;
            for (var i = 0; i < menuItems.Count; i++)
            {
                var menuItem = menuItems[i];
                var separator = (bool)menuItem.GetType().GetField("separator", BindingFlags.Instance | BindingFlags.Public).GetValue(menuItem);
                var content = menuItem.GetType().GetField("content", BindingFlags.Instance | BindingFlags.Public).GetValue(menuItem) as GUIContent;
                var func2 = menuItem.GetType().GetField("func2", BindingFlags.Instance | BindingFlags.Public).GetValue(menuItem) as Delegate;
                var userData = menuItem.GetType().GetField("userData", BindingFlags.Instance | BindingFlags.Public).GetValue(menuItem);
                //var on = (bool)menuItem.GetType().GetField("on", BindingFlags.Instance | BindingFlags.Public).GetValue(menuItem);

                var txt = content.text.Trim();
                if (separator || string.IsNullOrEmpty(txt) || txt.EndsWith("/")) continue;
                if (txt.Contains("/Dynamic ")) { isDynamic = true; continue; }
                if (txt.Contains("/Static Parameters")) { isDynamic = false; continue; }
                Action onClick = () => func2.GetInvocationList()[0].Method.Invoke(null, new[] { userData });

                list.Add(new eEventDialog.HackedMenuElement { isDynamic = isDynamic, text = txt, onClick = onClick });
            }

            return list.ToArray();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------

        private void ResetMethodInProperty(int indx)
        {
            _property.serializedObject.ApplyModifiedProperties();
            _property.serializedObject.Update();
            var p = _property.FindPropertyRelative("m_PersistentCalls.m_Calls").GetArrayElementAtIndex(indx).FindPropertyRelative("m_MethodName");
            p.stringValue = "";
        }

        //----------------------------------------------------------------------------------------------------------------------------------------
    }
}