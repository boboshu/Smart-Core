using System.IO;
using System.Reflection;
using UnityEngine;

namespace Smart.Utils
{
    public static class SysInfo
    {
        static SysInfo()
        {
            path = Application.dataPath;
            switch (Application.platform)
            {
                case RuntimePlatform.OSXPlayer: path = Path.GetFullPath(path + "/../../"); break;
                case RuntimePlatform.WindowsPlayer: path = Path.GetFullPath(path + "/../"); break;
                case RuntimePlatform.WindowsEditor: path = Path.GetFullPath(path + "/../"); break;
            }

            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                appVersion = entryAssembly.GetName().Version.ToString();
                appName = Path.GetFileNameWithoutExtension(entryAssembly.Location);
            }

            var executingAssembly = Assembly.GetExecutingAssembly();
            coreVersion = executingAssembly.GetName().Version.ToString();
            coreName = Path.GetFileNameWithoutExtension(executingAssembly.Location);
        }

        public static readonly string path;

        public static readonly string appVersion;
        public static readonly string appName;

        public static readonly string coreVersion;
        public static readonly string coreName;
    }
}