using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Smart.Setup
{
    public static class eSetup
    {
        public static void InitializeDLLWithType<T>(string package, string dllGuid, Action<eSetupIcons> onInitialize)
            where T : MonoBehaviour
        {
            var setupData = new eSetupIcons(package);
            setupData.metaFileBuilder.AppendLine("fileFormatVersion: 2\nguid: " + dllGuid + "\nPluginImporter:\n  serializedVersion: 1\n  iconMap:");
            onInitialize(setupData);            

            var fileName = GetAssemblyFileName<T>() + ".meta";
            if (File.Exists(fileName))
            {
                var fileText = File.ReadAllText(fileName);

                setupData.metaFileBuilder.Append(fileText.Substring(fileText.IndexOf("  executionOrder: ")));
                var newFileText = setupData.metaFileBuilder.ToString();
                if (newFileText != fileText)
                {
                    File.Delete(fileName);
                    File.WriteAllText(fileName, setupData.metaFileBuilder.ToString());
                }
            }            

            DisableScriptGizmos(setupData.disableGizmoForTypes);
        }

        private static string GetAssemblyFileName<T>()
            where T : MonoBehaviour
        {
            var codeBase = Assembly.GetAssembly(typeof(T)).CodeBase;
            var uri = new UriBuilder(codeBase);
            return Uri.UnescapeDataString(uri.Path);
        }

        private static void DisableScriptGizmos(HashSet<string> disableGizmoForTypes)
        {
            var isEnabled = 0;
            var asm = Assembly.GetAssembly(typeof(Editor));
            var type = asm.GetType("UnityEditor.AnnotationUtility");
            if (type == null) return;

            var getAnnotations = type.GetMethod("GetAnnotations", BindingFlags.Static | BindingFlags.NonPublic);
            var setGizmoEnabled = type.GetMethod("SetGizmoEnabled", BindingFlags.Static | BindingFlags.NonPublic);
            var setIconEnabled = type.GetMethod("SetIconEnabled", BindingFlags.Static | BindingFlags.NonPublic);
            var annotations = getAnnotations.Invoke(null, null);
            foreach (var annotation in (IEnumerable)annotations)
            {
                var annotationType = annotation.GetType();
                var classIdField = annotationType.GetField("classID", BindingFlags.Public | BindingFlags.Instance);
                var scriptClassField = annotationType.GetField("scriptClass", BindingFlags.Public | BindingFlags.Instance);
                    
                if (classIdField != null && scriptClassField != null)
                {
                    var classId = (int)classIdField.GetValue(annotation);
                    var scriptClass = (string)scriptClassField.GetValue(annotation);
                    if (!disableGizmoForTypes.Contains(scriptClass)) continue;
                    setGizmoEnabled.Invoke(null, new object[] { classId, scriptClass, isEnabled });
                    setIconEnabled.Invoke(null, new object[] { classId, scriptClass, isEnabled });
                }
            }
        }
    }
}
