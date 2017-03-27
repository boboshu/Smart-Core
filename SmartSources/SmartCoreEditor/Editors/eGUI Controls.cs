using System;
using System.Linq;
using Smart.Extensions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Smart.Editors
{
    public static partial class eGUI
    {
        public static void InspectorHeader(string text, bool hasErrors = false, bool hasWarnings = false)
        {
            EditorGUI.indentLevel--;
            GUI.backgroundColor = hasErrors ? Color.red : (hasWarnings ? Color.yellow : Color.blue);
            EditorGUILayout.HelpBox(text, hasErrors ? MessageType.Error : (hasWarnings ? MessageType.Warning : MessageType.None));
            GUI.backgroundColor = background;
            EditorGUI.indentLevel++;
        }

        public static void Header(string text, bool hasErrors = false, bool hasWarnings = false)
        {
            GUI.backgroundColor = hasErrors ? Color.red : (hasWarnings ? Color.yellow : Color.blue);
            EditorGUILayout.HelpBox(text, hasErrors ? MessageType.Error : (hasWarnings ? MessageType.Warning : MessageType.None));
            GUI.backgroundColor = background;
        }

        public static void InfoBlock(string text)
        {
            EditorGUILayout.HelpBox(text, MessageType.Info);
        }

        public static void TextBlock(string text)
        {
            EditorGUILayout.HelpBox(text, MessageType.None);
        }

        public static void PropCollection<T>(SerializedProperty items_prop, ref T[] items, Action<T, SerializedProperty, int> onDrawItem, eCollection options)
        {
            for (var i = 0; i < items.Length; i++)
            {
                GUI.backgroundColor = i % 2 == 0 ? new Color(0.7f, 0.7f, 0.7f) : new Color(1, 1, 1);
                var item = items[i];

                if (!options.HasSingleLine()) LineDelimiter();
                GUILayout.BeginHorizontal();
                if (!options.HasSingleLine())
                {
                    GUILayout.BeginVertical();
                }

                if (i < items_prop.arraySize)
                    onDrawItem(item, items_prop == null ? null : items_prop.GetArrayElementAtIndex(i), i);

                if (!options.HasSingleLine() || options.HasVerticalTools())
                {
                    if (!options.HasSingleLine()) GUILayout.EndVertical();
                    GUILayout.BeginVertical(GUILayout.Width(MINI_BUTTON_WIDTH));
                }

                if (options.HasGotoRef() && item is Object && ButtonMini("≡", "Goto This Item", cyanLt, MINI_BUTTON_WIDTH))
                {
                    Selection.activeObject = item as Object;
                }

                if (options.HasReorder())
                {
                    if (i > 0)
                    {
                        if (ButtonMini("▲", "Move Up", azureLt, MINI_BUTTON_WIDTH)) items.MoveBack(item);
                    }
                    else
                    {
                        ButtonMini("▲", "Move Up (Not Allowed)", Color.clear, MINI_BUTTON_WIDTH); // placeholder
                    }
                }

                if (options.HasReorder())
                {
                    if (i < items.Length - 1)
                    {
                        if (ButtonMini("▼", "Move Down", azureLt, MINI_BUTTON_WIDTH)) items.MoveForw(item);
                    }
                    else
                    {
                        ButtonMini("▼", "Move Down (Not Allowed)", Color.clear, MINI_BUTTON_WIDTH); // placeholder
                    }
                }

                if (options.HasDelete() && ButtonMini(eIcons.Get("d_winbtn_win_close"), "Delete Item", redLt, MINI_BUTTON_WIDTH))
                {
                    items = items.Remove(item);
                    if (options.HasDestroyRef())
                    {
                        if (item is Component) Object.DestroyImmediate((item as Component).gameObject);
                        else if (item is GameObject) Object.DestroyImmediate(item as GameObject);
                    }
                    return;
                }
                if (!options.HasSingleLine() || options.HasVerticalTools()) GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }
            GUI.backgroundColor = background;
        }

        public static void Collection<T>(ref T[] items, Action<T, int> onDrawItem, eCollection options)
        {
            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                GUILayout.BeginHorizontal();
                if (!options.HasSingleLine()) GUILayout.BeginVertical();

                onDrawItem(item, i);

                if (!options.HasSingleLine())
                {
                    GUILayout.EndVertical();
                    GUILayout.BeginVertical(GUILayout.Width(MINI_BUTTON_WIDTH));
                }

                if (options.HasGotoRef() && item is Object && ButtonMini("≡", "Goto This Item", cyanLt, MINI_BUTTON_WIDTH))
                {
                    Selection.activeObject = item as Object;
                }

                if (options.HasReorder())
                {
                    if (i > 0)
                    {
                        if (ButtonMini("▲", "Move Up", azureLt, MINI_BUTTON_WIDTH)) items.MoveBack(item);
                    }
                    else
                    {
                        ButtonMini("▲", "Move Up (Not Allowed)", Color.clear, MINI_BUTTON_WIDTH); // placeholder
                    }
                }

                if (options.HasReorder())
                {
                    if (i < items.Length - 1)
                    {
                        if (ButtonMini("▼", "Move Down", azureLt, MINI_BUTTON_WIDTH)) items.MoveForw(item);
                    }
                    else
                    {
                        ButtonMini("▼", "Move Down (Not Allowed)", Color.clear, MINI_BUTTON_WIDTH); // placeholder
                    }
                }

                if (options.HasDelete() && ButtonMini(eIcons.Get("d_winbtn_win_close"), "Delete Item", redLt, MINI_BUTTON_WIDTH))
                {
                    items = items.Remove(item);
                    if (options.HasDestroyRef())
                    {
                        if (item is Component) Object.DestroyImmediate((item as Component).gameObject);
                        else if (item is GameObject) Object.DestroyImmediate(item as GameObject);
                    }
                    return;
                }
                if (!options.HasSingleLine()) GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                if (!options.HasSingleLine()) LineDelimiter();
            }
        }

        public static void CollectionToolbar<T>(ref T[] items, Func<T> onNewItem, string text = null, bool destroyRef = false)
        {
            GUILayout.BeginHorizontal();
            if (text == null) GUILayout.FlexibleSpace();
            else Header(text);

            if (items.Length > 0 && ButtonMini(eIcons.Get("d_treeeditor.trash"), "Delete All Items", redLt))
            {
                if (EditorUtility.DisplayDialog("Are you sure?", "Do you want to clear all items?", "Yes", "No"))
                {
                    if (destroyRef)
                    {
                        foreach (var cmp in items.OfType<Component>())
                            Object.DestroyImmediate(cmp.gameObject);
                    }
                    items = new T[0];
                }
            }

            if (onNewItem != null && ButtonMini(eIcons.Get("d_toolbar plus"), "Create New Item", greenLt))
            {
                var item = onNewItem();
                var lst = items.ToList();
                lst.Add(item);
                items = lst.ToArray();
            }

            GUILayout.EndHorizontal();
        }

        public static void PropEditor(SerializedProperty property, bool haveError = false, bool haveWarning = false, string caption = null)
        {
            BeginColors();
            if (haveError)
            {
                SetColor(Color.red);
                if (caption == null) EditorGUILayout.PropertyField(property);
                else EditorGUILayout.PropertyField(property, new GUIContent(caption));
            }
            else if (haveWarning)
            {
                SetColor(Color.yellow);
                if (caption == null) EditorGUILayout.PropertyField(property);
                else EditorGUILayout.PropertyField(property, new GUIContent(caption));
            }
            else
            {
                if (caption == null) EditorGUILayout.PropertyField(property);
                else EditorGUILayout.PropertyField(property, new GUIContent(caption));
            }
            EndColors();
        }

        public static void PropEditor(SerializedProperty property, string caption)
        {
            EditorGUILayout.PropertyField(property, new GUIContent(caption));
        }

        public static void EmptySpace()
        {
            EditorGUILayout.Space();
        }

        public static void LineDelimiter()
        {
            GUILayout.Space(10f);

            if (Event.current.type != EventType.Repaint) return;

            var tex = EditorGUIUtility.whiteTexture;
            var rect = GUILayoutUtility.GetLastRect();

            var clr = GUI.color;
            GUI.color = new Color(0f, 0f, 0f, 0.15f);

            GUI.DrawTexture(new Rect(0f, rect.yMin + 3f, Screen.width, 4f), tex);
            GUI.DrawTexture(new Rect(0f, rect.yMin + 3f, Screen.width, 1f), tex);
            GUI.DrawTexture(new Rect(0f, rect.yMin + 6f, Screen.width, 1f), tex);

            GUI.color = clr;
        }

        public static GUILayoutOption[] SizeOptions(int width = 0, int height = 0) // -1 to collapse, 0 to expand, N to exact size
        {
            return new[]
            {
                width > 0 ? GUILayout.Width(width) : GUILayout.ExpandWidth(width == 0),
                height > 0 ? GUILayout.Height(height) : GUILayout.ExpandHeight(height == 0)
            };
        }

        public static bool Button(string text, int width = 0, int height = 18)
        {
            return GUILayout.Button(text, SizeOptions(width, height));
        }

        public static bool Button(string text, Color clr, int width = 0, int height = 18)
        {
            BeginColors();
            SetBGColor(clr);
            var b = GUILayout.Button(text, SizeOptions(width, height));
            EndColors();
            return b;
        }

        public static bool Button(string text, Texture2D icon, Color clr, int width = 0, int height = 18) // width = -1 will minimize width
        {
            BeginColors();
            SetBGColor(clr);
            var b = GUILayout.Button(new GUIContent(text, icon), SizeOptions(width, height));
            EndColors();
            return b;
        }

        public static bool Button(Texture2D icon, string tooltip, Color clr, int width = 0, int height = 18) // width = -1 will minimize width
        {
            BeginColors();
            SetBGColor(clr);
            var b = GUILayout.Button(new GUIContent(icon, tooltip), SizeOptions(width, height));
            EndColors();
            return b;
        }

        public static bool Button(string text, string tooltip, Color clr, int width = 0, int height = 18) // width = -1 will minimize width
        {
            BeginColors();
            SetBGColor(clr);
            var b = GUILayout.Button(new GUIContent(text, tooltip), SizeOptions(width, height));
            EndColors();
            return b;
        }

        private static GUIStyle _buttonMiniStyle;
        private static GUIStyle _buttonMiniStyleEx;

        private static GUIStyle GetButtonMiniStyle()
        {
            return _buttonMiniStyle ?? (_buttonMiniStyle = new GUIStyle(GUI.skin.button) { padding = new RectOffset(1, 1, 0, 0), overflow = new RectOffset(2, 2, 0, 1), fontSize = 10 });
        }

        private static GUIStyle GetButtonMiniStyleEx()
        {
            return _buttonMiniStyleEx ?? (_buttonMiniStyleEx = new GUIStyle(GUI.skin.button) { padding = new RectOffset(1, 1, -3, 0), overflow = new RectOffset(2, 2, 0, 1), fontSize = 10 });
        }

        public static bool ButtonMini(string text, int width = -1, int height = 16)
        {
            return GUILayout.Button(text, GetButtonMiniStyleEx(), SizeOptions(width, height));
        }

        public static bool ButtonMini(string text, Texture2D icon, int width = -1, int height = 16)
        {
            return GUILayout.Button(new GUIContent(text, icon), GetButtonMiniStyle(), SizeOptions(width, height));
        }

        public static bool ButtonMini(string text, Color clr, int width = -1, int height = 16)
        {
            BeginColors();
            SetBGColor(clr);
            var b = GUILayout.Button(text, GetButtonMiniStyleEx(), SizeOptions(width, height));
            EndColors();
            return b;
        }

        public static bool ButtonMini(string text, Texture2D icon, Color clr, int width = -1, int height = 16) // width = -1 will minimize width
        {
            BeginColors();
            SetBGColor(clr);
            var b = GUILayout.Button(new GUIContent(text, icon), GetButtonMiniStyle(), SizeOptions(width, height));
            EndColors();
            return b;
        }

        public static bool ButtonMini(string text, Texture2D icon, string tooltip, Color clr, int width = -1, int height = 16) // width = -1 will minimize width
        {
            BeginColors();
            SetBGColor(clr);
            var b = GUILayout.Button(new GUIContent(text, icon, tooltip), GetButtonMiniStyle(), SizeOptions(width, height));
            EndColors();
            return b;
        }

        public static bool ButtonMini(Texture2D icon, string tooltip, Color clr, int width = -1, int height = 16) // width = -1 will minimize width
        {
            BeginColors();
            SetBGColor(clr);
            var b = GUILayout.Button(new GUIContent(icon, tooltip), GetButtonMiniStyle(), SizeOptions(width, height));
            EndColors();
            return b;
        }

        public static bool ButtonMini(string text, string tooltip, Color clr, int width = -1, int height = 16) // width = -1 will minimize width
        {
            BeginColors();
            SetBGColor(clr);
            var b = GUILayout.Button(new GUIContent(text, tooltip), GetButtonMiniStyleEx(), SizeOptions(width, height));
            EndColors();
            return b;
        }

        public static bool FoldOut(ref bool foldOut, string text)
        {
            var c = new GUIContent(foldOut ? "−−−[ " + text + " ]−−−" : "≡≡≡[ " + text + " ]≡≡≡", foldOut ? "Click to Collapse" : "Click to Expand");
            if (GUILayout.Button(c, new GUIStyle(GUI.skin.button) { fontStyle = FontStyle.Bold }, GUILayout.ExpandWidth(true))) foldOut = !foldOut;
            return foldOut;
        }

        public static bool FoldOut(ref bool foldOut, string text, Color clr)
        {
            BeginColors();
            SetColor(clr);
            FoldOut(ref foldOut, text);
            EndColors();
            return foldOut;
        }

        public static void Pages(ref byte indx, Color color, params string[] names)
        {
            BeginColors();
            SetColor(color);
            indx = (byte)GUILayout.Toolbar(indx, names);
            EndColors();
        }

        public static void PagesGrid(ref byte indx, Color color, int xCount, params string[] names)
        {
            BeginColors();
            SetColor(color);
            indx = (byte)GUILayout.SelectionGrid(indx, names, xCount);
            EndColors();
        }

        public static void Switch(ref byte indx, Color color, params string[] names)
        {
            BeginColors();
            SetColor(color);
            indx = (byte)GUILayout.Toolbar(indx, names, GUILayout.ExpandWidth(false));
            EndColors();
        }

        public static void LabelBold(string text, bool expandW = true, bool expandH = false)
        {
            GUILayout.Label(text, new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleLeft }, GUILayout.ExpandWidth(expandW), GUILayout.ExpandHeight(expandH));
        }

        public static void LabelBold(string text, Color clr)
        {
            BeginColors();
            SetColor(clr);
            GUILayout.Label(text, EditorStyles.boldLabel);
            EndColors();
        }

        public static void TextEditor(ref string text, string title = null)
        {
            if (string.IsNullOrEmpty(title))
            {
                text = EditorGUILayout.TextField(text ?? "");
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Label(title);
                    text = EditorGUILayout.TextField(text ?? "");
                }
                EditorGUILayout.EndHorizontal();               
            }
        }

        public static void IntEditor(ref int val, string title = null, int width = 0, int height = -1)
        {
            val = EditorGUILayout.IntField(title ?? "", val, SizeOptions(width, height));
        }

        public static void IntEditor(ref byte val, string title = null)
        {
            val = (byte)EditorGUILayout.IntField(title ?? "", val);
        }

        public static void FloatEditor(ref float val, string label = null)
        {
            val = EditorGUILayout.FloatField(label ?? "", val);
        }

        public static void Vector3Editor(ref Vector3 vec, string label = null)
        {
            vec = EditorGUILayout.Vector3Field(label ?? "", vec);
        }

        public static void EnumEditor<T>(ref T enm, string label = null)
            where T : struct, IConvertible
        {
            var e = EditorGUILayout.EnumPopup(label ?? "", enm as Enum);
            enm = (T)Convert.ChangeType(e, typeof(T));
        }

        public static void EnumEditorWithSorting<T>(ref T enm, Color color, string label = null)
            where T : struct, IConvertible
        {
            BeginColors();
            SetColor(greenLt);

            var names = Enum.GetNames(typeof(T)).Where(x => !x.StartsWith("_")).Select(x => x.Replace('_', '/')).ToArray();
            var sortedNames = names.OrderBy(x => x).ToArray();

            var oldDirectIndex = Convert.ToInt32(enm);
            var oldSortedIndx = sortedNames.IndexOf(names[oldDirectIndex]);
            var newSortedIndx = EditorGUILayout.Popup(label, oldSortedIndx, sortedNames);

            if (oldSortedIndx != newSortedIndx && newSortedIndx >= 0 && newSortedIndx < sortedNames.Length)
            {
                var newDirectIndex = names.IndexOf(sortedNames[newSortedIndx]);
                if (newDirectIndex >= 0 && newDirectIndex < names.Length)
                    enm = (T)Enum.ToObject(typeof(T), newDirectIndex);
            }
            EndColors();
        }

        public static void CheckBoxEditor(bool val, Action<bool> setter, string label = null)
        {
            var v = EditorGUILayout.Toggle(label ?? "", val);
            if (v != val) setter(v);
        }

        public static void CheckBoxEditor(ref bool val, string label = null)
        {
            var v = EditorGUILayout.Toggle(label ?? "", val);
            if (v != val) val = v;
        }

        public static void CheckBoxExEditor(bool val, Action<bool> setter, string label = null)
        {
            GUILayout.BeginHorizontal();
            var v = EditorGUILayout.Toggle(val, GUILayout.Width(16));
            if (label != null) GUILayout.Label(label);
            GUILayout.EndHorizontal();
            if (v != val) setter(v);
        }

        public static void CheckBoxExEditor(ref bool val, string label = null)
        {
            GUILayout.BeginHorizontal();
            var v = EditorGUILayout.Toggle(val, GUILayout.Width(16));
            if (label != null) GUILayout.Label(label);
            GUILayout.EndHorizontal();
            if (v != val) val = v;
        }

        public static void ComboEditor(ref string val, params string[] items)
        {
            var indx = 0;
            foreach (var item in items)
            {
                if (item == val) break;
                indx++;
            }
            if (indx == items.Length) indx = 0;
            var i = EditorGUILayout.Popup(indx, items);
            if (i >= 0 && i < items.Length) val = items[i];
        }

        public static void ComboEditor(ref string val, string label, params string[] items)
        {
            var indx = 0;
            foreach (var item in items)
            {
                if (item == val) break;
                indx++;
            }
            if (indx == items.Length) indx = 0;
            var i = EditorGUILayout.Popup(label, indx, items);
            if (i >= 0 && i < items.Length) val = items[i];
        }

        public static void SliderEditor(ref float val, float min, float max, string label = null)
        {
            val = EditorGUILayout.Slider(label ?? "", val, min, max);
        }

        public static void SliderEditor(ref int val, int min, int max, string label = null)
        {
            val = EditorGUILayout.IntSlider(label ?? "", val, min, max);
        }

        public static bool DialogQuery(string title, string message)
        {
            return EditorUtility.DisplayDialog(title, message, "Ok", "Cancel");
        }

        public static bool DialogQuery(string message)
        {
            return EditorUtility.DisplayDialog("Confirmation", message, "Ok", "Cancel");
        }
    }
}