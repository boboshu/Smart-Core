using System;
using System.Linq;
using Smart.Editors;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Smart.Custom
{
    public static class eCustomEditors
    {
        public static void DrawBoolean(SerializedProperty property, string title = null)
        {
            property.boolValue = GUILayout.Toggle(property.boolValue, title ?? property.displayName);
        }

        private static GUIStyle _iconStyle;

        private static void PrepareIconStyle()
        {
            if (_iconStyle != null) return;
            _iconStyle = new GUIStyle(GUI.skin.label) { margin = new RectOffset(), padding = new RectOffset(), overflow = new RectOffset() };
        }

        public static void DrawWithIcons(Action drawContent, bool error, bool warning = false)
        {
            if (error || warning)
            {
                GUILayout.BeginHorizontal();
                {
                    drawContent();
                    if (error) DrawErrorIcon();
                    else DrawWarningIcon();
                }
                GUILayout.EndHorizontal();
            }
            else
            {
                drawContent();
            }
        }

        public static void DrawWithIcons(SerializedProperty property, bool error, bool warning = false)
        {
            DrawWithIcons(() => EditorGUILayout.PropertyField(property), error, warning);
        }

        public static void DrawWithIcons(SerializedProperty property, string propertyTitle, bool error, bool warning = false)
        {
            DrawWithIcons(() => EditorGUILayout.PropertyField(property, new GUIContent(propertyTitle)), error, warning);
        }

        public static void DrawErrorIcon()
        {
            PrepareIconStyle();
            var fg = GUI.contentColor;
            GUI.contentColor = Color.white;
            GUILayout.Label(eIcons.Get("icons/d_console.erroricon.sml.png"), _iconStyle, GUILayout.Width(16));
            GUI.contentColor = fg;
        }

        public static void DrawWarningIcon()
        {
            PrepareIconStyle();
            var fg = GUI.contentColor;
            GUI.contentColor = Color.white;
            GUILayout.Label(eIcons.Get("icons/d_console.warnicon.sml.png"), _iconStyle, GUILayout.Width(16));
            GUI.contentColor = fg;
        }

        public static void DrawObjectProperty(SerializedProperty property, string title = null, bool required = true, bool prefab = false)
        {
            DrawWithIcons(() =>
            {
                eGUI.BeginColors();
                eGUI.SetColor(eGUI.violet);
                if (title == null) EditorGUILayout.PropertyField(property); // use default
                else EditorGUILayout.PropertyField(property, title == "" ? GUIContent.none : new GUIContent(title));
                eGUI.EndColors();
            }, (required && property.objectReferenceValue == null) || (prefab && PrefabUtility.GetPrefabType(property.objectReferenceValue) != PrefabType.Prefab));
        }

        public static void DrawObjectProperty<T>(SerializedProperty property, T[] checkForDuplicates, string title = null, bool required = true, bool prefab = false)
            where T : Object
        {
            DrawWithIcons(() =>
            {
                eGUI.BeginColors();
                eGUI.SetColor(eGUI.violet);
                if (title == null) EditorGUILayout.PropertyField(property); // use default
                else EditorGUILayout.PropertyField(property, title == "" ? GUIContent.none : new GUIContent(title));
                eGUI.EndColors();
            }, (required && property.objectReferenceValue == null) || (prefab && PrefabUtility.GetPrefabType(property.objectReferenceValue) != PrefabType.Prefab) || (checkForDuplicates.Count(x => x == (T)property.objectReferenceValue) > 1));
        }

        public static void DrawCommandProperty(SerializedProperty property, string title = null, bool required = false)
        {
            DrawWithIcons(() =>
            {
                eGUI.BeginColors();
                eGUI.SetColor(Color.yellow);
                if (title == null) EditorGUILayout.PropertyField(property); // use default
                else EditorGUILayout.PropertyField(property, title == "" ? GUIContent.none : new GUIContent(title));
                eGUI.EndColors();
            }, required && property.objectReferenceValue == null);
        }

        public static void DrawObjectsCollection<T>(ref T[] collection, SerializedProperty property, string title, bool duplicatesNotAllowed = true)
            where T : class
        {
            var cc = collection;
            eGUI.CollectionToolbar(ref collection, () => null, title);
            eGUI.PropCollection(property, ref collection, (v, p, i) =>
            {
                eGUI.BeginColors();
                eGUI.SetColor(eGUI.violet);
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("#" + (i + 1), GUILayout.ExpandWidth(false));
                    if (v == null) DrawErrorIcon();
                    else if (duplicatesNotAllowed && cc.Count(x => x == v) > 1) DrawErrorIcon();
                    EditorGUILayout.PropertyField(p, GUIContent.none);
                }
                GUILayout.EndHorizontal();
                eGUI.EndColors();
            }, eCollection.Delete_SingleLine_Reorder);
        }

        public static void DrawMemoField(SerializedProperty property, string title = null, int height = 0)
        {
            GUILayout.BeginHorizontal();
            var propertyValue = property.stringValue;
            {
                if (eGUI.ButtonMini((title ?? ObjectNames.NicifyVariableName(property.name)) + " [Open]", eGUI.azure, 0))
                    eTextDialog.Execute(propertyValue, s =>
                    {
                        property.stringValue = s;
                        property.serializedObject.ApplyModifiedProperties();
                    });
                if (eGUI.ButtonMini("", eIcons.Get("icons/clipboard.png"), eGUI.azure, 32)) EditorGUIUtility.systemCopyBuffer = propertyValue;
                if (eGUI.ButtonMini("", eIcons.Get("icons/vcs_document.png"), eGUI.azure, 32)) property.stringValue = EditorGUIUtility.systemCopyBuffer;
            }
            GUILayout.EndHorizontal();

            var stl = new GUIStyle(GUI.skin.textArea) { wordWrap = true, clipping = TextClipping.Clip };
            property.stringValue = (height == 0) ? EditorGUILayout.TextArea(propertyValue, stl) : EditorGUILayout.TextArea(propertyValue, stl, GUILayout.Height(height));
        }

        public static void DrawProperty(Color color, SerializedProperty property, string title = null, params GUILayoutOption[] options)
        {
            eGUI.BeginColors();
            eGUI.SetColor(color);
            if (title == null) EditorGUILayout.PropertyField(property); // use default
            else EditorGUILayout.PropertyField(property, title == "" ? GUIContent.none : new GUIContent(title), options);
            eGUI.EndColors();
        }
    }
}