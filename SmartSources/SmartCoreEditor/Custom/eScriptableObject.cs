using System.IO;
using UnityEditor;
using UnityEngine;

namespace Smart.Custom
{
    public static class eScriptableObject
    {
        public static T CreateAssetInResources<T>(string resourcesFolder, string subFolder, bool setFocus = true, string name = "Noname") where T : ScriptableObject
        {
            var asset = ScriptableObject.CreateInstance<T>();
            var assetPathAndName = "Assets/" + resourcesFolder + "Resources/" + subFolder + "/" + name + ".asset";
            Directory.CreateDirectory(Path.GetDirectoryName(assetPathAndName.Replace('/', '\\')));

            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Selection.activeObject = asset;
            if (setFocus) EditorUtility.FocusProjectWindow();
            return asset;
        }

        public static void CreateAssetInPlace<T>(string name = null) where T : ScriptableObject
        {
            var asset = ScriptableObject.CreateInstance<T>();
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (string.IsNullOrEmpty(path)) path = "Assets";
            else if (Path.GetExtension(path) != "")
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");

            var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + (name ?? typeof(T).Name) + ".asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Selection.activeObject = asset;
            EditorUtility.FocusProjectWindow();
        }
    }
}