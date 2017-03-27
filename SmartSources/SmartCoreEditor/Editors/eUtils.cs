using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Smart.Editors
{
    public static class eUtils
    {
        public static void ClearConsoleLog()
        {
            var SampleAssembly = Assembly.GetAssembly(typeof(BlendTree));
            if (SampleAssembly == null) return;

            var type = SampleAssembly.GetType("UnityEditorInternal.LogEntries");
            if (type == null) return;

            var method = type.GetMethod("Clear");
            if (method == null) return;

            method.Invoke(new object(), null);
        }

        public static Type[] FindDomainClasses<T>()
            where T : class
        {
            var r = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assembly.FullName.StartsWith("Assembly-")))
                r.AddRange(assembly.GetTypes().Where(t => t.IsClass && t.IsSubclassOf(typeof(T))));
            return r.ToArray();
        }

        public static bool IsCompiling()
        {
            if (EditorApplication.isCompiling)
            {
                EditorUtility.DisplayDialog("Unity is busy", "Please wait until Unity is compiling scripts", "Ok");
                return true;
            }
            return false;
        }

        public static void HierarchyFoldAll(GameObject go, bool expand)
        {
            var type = typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneHierarchyWindow");
            var methodInfo = type.GetMethod("SetExpandedRecursive");

            if (hierarchyWindow == null)
            {
                EditorApplication.ExecuteMenuItem("Window/Hierarchy");
                hierarchyWindow = EditorWindow.focusedWindow;
            }

            methodInfo.Invoke(hierarchyWindow, new object[] {go.GetInstanceID(), expand});
        }

        public static void HierarchyFold(GameObject go, bool expand)
        {
            if (go.transform.childCount == 0) return;
            if (hierarchyWindow == null)
            {
                EditorApplication.ExecuteMenuItem("Window/Hierarchy");
                hierarchyWindow = EditorWindow.focusedWindow;
            }
            Selection.activeObject = go;
            hierarchyWindow.SendEvent(new Event {keyCode = expand ? KeyCode.RightArrow : KeyCode.LeftArrow, type = EventType.keyDown});
        }

        private static EditorWindow hierarchyWindow;
    }
}