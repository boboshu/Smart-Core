using System.Linq;
using System.Reflection;
using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Extensions
{
    public static class eExtraCommands
    {
        private static EditorWindow _inspectorWindow;

        //----------------------------------------------------------------------------------------------------------------------------------

        [MenuItem("Tools/Smart/Toggle Inspector Lock #&Z", false, 1001)]
        private static void ToggleInspectorLock()
        {
            if (_inspectorWindow == null)
            {
                var type = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");
                var findObjectsOfTypeAll = Resources.FindObjectsOfTypeAll(type);
                _inspectorWindow = (EditorWindow)findObjectsOfTypeAll.FirstOrDefault(x => x.GetType().Name == "InspectorWindow");
            }

            if (_inspectorWindow)
            {
                var type = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");
                var propertyInfo = type.GetProperty("isLocked");
                var value = (bool)propertyInfo.GetValue(_inspectorWindow, null);
                propertyInfo.SetValue(_inspectorWindow, !value, null);
                _inspectorWindow.Repaint();
            }
        }

        [MenuItem("Tools/Smart/Toggle Activate Object #&A", false, 1002)]
        private static void ToggleActivateObject()
        {
            var go = Selection.activeGameObject;
            if (go) go.SetActive(!go.activeSelf);
        }

        [MenuItem("Tools/Smart/Clear Selection #&S", false, 1003)]
        private static void ClearSelection()
        {
            Selection.activeGameObject = null;
        }

        [MenuItem("Tools/Smart/Clear Console #&C", false, 1004)]
        private static void ClearConsole()
        {
            eUtils.ClearConsoleLog();
        }

        [MenuItem("Tools/Smart/New GameObject #&N", false, 1005)]
        private static void CreateGameObject()
        {
            var go = new GameObject("GameObject");
            go.transform.parent = Selection.activeTransform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            Undo.RegisterCreatedObjectUndo(go, "Create New GameObject");
            Selection.activeGameObject = go;
        }

        [MenuItem("Tools/Smart/Scenes Dialog #&X", false, 4000)]
        private static void ScenesDialog()
        {
            eScenesDialog.Toggle();
        }

        [MenuItem("Assets/Open Visual Studio #&V", false, 4001)]
        private static void OpenVisualStudio()
        {
            EditorApplication.ExecuteMenuItem("Assets/Open C# Project");
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        [MenuItem("Tools/Smart/Player Settings #&P", false, 2000)]
        private static void PlayerSettings()
        {
            EditorApplication.ExecuteMenuItem("Edit/Project Settings/Player");
        }

        [MenuItem("Tools/Smart/Input Settings #&I", false, 2001)]
        private static void InputSettings()
        {
            EditorApplication.ExecuteMenuItem("Edit/Project Settings/Input");
        }

        [MenuItem("Tools/Smart/Quality Settings #&Q", false, 2002)]
        private static void QualitySettings()
        {
            EditorApplication.ExecuteMenuItem("Edit/Project Settings/Quality");
        }

        [MenuItem("Tools/Smart/Tags and Layers Settings #&T", false, 2003)]
        private static void TagsAndLayersSettings()
        {
            EditorApplication.ExecuteMenuItem("Edit/Project Settings/Tags and Layers");
        }

        [MenuItem("Tools/Smart/Editor Settings #&E", false, 2004)]
        private static void EditorSettings()
        {
            EditorApplication.ExecuteMenuItem("Edit/Project Settings/Editor");
        }

        //----------------------------------------------------------------------------------------------------------------------------------
    }
}