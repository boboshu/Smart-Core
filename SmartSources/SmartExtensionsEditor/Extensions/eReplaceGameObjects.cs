using UnityEditor;
using UnityEngine;

namespace Smart.Extensions
{
    public class eReplaceGameObjects : ScriptableWizard
    {
        private static eReplaceGameObjects _window;

        public GameObject ReplaceByObject;
        public GameObject[] ObjectsToReplace;

        [MenuItem("Tools/Smart/Replace GameObjects #&R", false, 4002)]
        private static void MainMenuItem()
        {
            if (_window) _window.Close();
            else
            {
                _window = DisplayWizard<eReplaceGameObjects>("Replace GameObjects", "Replace");
                _window.ObjectsToReplace = Selection.gameObjects;
            }
        }

        void OnWizardCreate()
        {
            foreach (var go in ObjectsToReplace)
            {
                var newObject = (GameObject)PrefabUtility.InstantiatePrefab(ReplaceByObject);
                newObject.transform.parent = go.transform.parent;
                newObject.transform.localPosition = go.transform.localPosition;
                newObject.transform.localRotation = go.transform.localRotation;
                newObject.transform.localScale = go.transform.localScale;
                DestroyImmediate(go);
            }
        }
    }
}
