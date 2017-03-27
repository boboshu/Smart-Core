using System.Collections.Generic;
using System.Text;
using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Setup
{
    public class eSetupIcons
    {
        public readonly StringBuilder metaFileBuilder = new StringBuilder();
        public readonly HashSet<string> disableGizmoForTypes = new HashSet<string>();
        public readonly string package;

        public eSetupIcons(string package)
        {
            this.package = package;
        }

        public void Reg<T>(string image, eHierarchyIcons.Priority priority)
            where T : MonoBehaviour
        {
            var path = "Assets/Smart/" + package + "/Editor/Icons/" + image + ".png";
            var guid = AssetDatabase.AssetPathToGUID(path);
            if (string.IsNullOrEmpty(guid)) return;

            var type = typeof(T);
            var typeName = string.IsNullOrEmpty(type.Namespace) ? type.Name : type.Namespace + "." + type.Name;
            metaFileBuilder.AppendLine("    " + typeName + ": {fileID: 2800000, guid: " + guid + ", type: 3}");
            eHierarchyIcons.Reg<T>(path, priority);
            disableGizmoForTypes.Add(type.Name);
        }

        public void Reg<T>(string image)
            where T : ScriptableObject
        {
            var path = "Assets/Smart/" + package + "/Editor/Icons/" + image + ".png";
            var guid = AssetDatabase.AssetPathToGUID(path);
            if (string.IsNullOrEmpty(guid)) return;

            var type = typeof(T);
            var typeName = string.IsNullOrEmpty(type.Namespace) ? type.Name : type.Namespace + "." + type.Name;
            metaFileBuilder.AppendLine("    " + typeName + ": {fileID: 2800000, guid: " + guid + ", type: 3}");
        }
    }
}
