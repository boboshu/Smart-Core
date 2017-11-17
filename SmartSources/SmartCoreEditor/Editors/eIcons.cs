using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Smart.Editors
{
    public static class eIcons
    {
        public static Texture2D Get(string path)
        {
            Texture2D tex;
            if (_cache.TryGetValue(path, out tex))
            {
                if (tex) return tex;
                _cache.Clear();
            }
            
            tex = (EditorGUIUtility.Load(path) ?? EditorGUIUtility.Load("icons/" + path + ".png")) as Texture2D;
            if (tex == null && path != "icons/d_sceneviewfx.png") tex = Get("icons/d_sceneviewfx.png");

            _cache.Add(path, tex);
            return tex;
        }

        public static Texture2D GetForScript<T>()
            where T : class
        {
            return GetForScript(typeof(T).Name);
        }

        public static Texture2D GetForScript(string className)
        {
            var name = '#' + className;
            Texture2D tex;
            if (_cache.TryGetValue(name, out tex)) return tex;

            var foundResults = AssetDatabase.FindAssets(className).Select(AssetDatabase.GUIDToAssetPath).ToArray();

            var fileName = "/" + className + ".cs";
            var path = foundResults.FirstOrDefault(x => x.EndsWith(fileName));
            if (path == null)
            {
                fileName = "/" + className + ".js";
                path = foundResults.FirstOrDefault(x => x.EndsWith(fileName));
            }

            if (path != null)
            {
                tex = AssetDatabase.GetCachedIcon(path) as Texture2D;
                if (tex != null)
                {
                    var tn = tex.name.ToLower();
                    if (tn == "defaultasset icon") tex = null; // do not use standard asset icon
                    if (tn == "cs script icon") tex = null; // do not use standard script icon
                    if (tn == "js script icon") tex = null; // do not use standard asset icon
                    if (tn == "boo script icon") tex = null; // do not use standard asset icon
                    if (tn == "scriptableobject icon") tex = null; // do not use standard asset icon
                }
            }
            _cache.Add(name, tex);
            return tex;
        }

        private static readonly Dictionary<string, Texture2D> _cache = new Dictionary<string, Texture2D>();
    }
}