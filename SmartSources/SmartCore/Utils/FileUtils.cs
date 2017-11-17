using System;
using System.IO;
using System.Linq;
using Smart.Extensions;

namespace Smart.Utils
{
    public static class FileUtils
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public static void CopyDirectory(string srcDir, string dstDir)
        {
            var dirInfo = new DirectoryInfo(srcDir);
            if (!dirInfo.Exists) return;

            var subFiles = dirInfo.GetFiles();
            var subDirectiories = dirInfo.GetDirectories();
            if (subFiles.Length == 0 && subDirectiories.Length == 0) return;

            Directory.CreateDirectory(dstDir);
            subFiles.Do(f => f.CopyTo(Path.Combine(dstDir, f.Name), true));
            subDirectiories.Do(subDir => CopyDirectory(subDir.FullName, Path.Combine(dstDir, subDir.Name)));
        }

        public static void CopyDirectory(string srcDir, string dstDir, Func<FileInfo, bool> filter)
        {
            var dirInfo = new DirectoryInfo(srcDir);
            if (!dirInfo.Exists) return;

            var subFiles = dirInfo.GetFiles();
            var subDirectiories = dirInfo.GetDirectories();
            if (subFiles.Length == 0 && subDirectiories.Length == 0) return;

            Directory.CreateDirectory(dstDir);
            subFiles.Where(filter).Do(f => f.CopyTo(Path.Combine(dstDir, f.Name), true));
            subDirectiories.Do(subDir => CopyDirectory(subDir.FullName, Path.Combine(dstDir, subDir.Name), filter));
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static long DirectorySize(string dir)
        {
            var dirInfo = new DirectoryInfo(dir);
            return dirInfo.Exists ? dirInfo.GetFiles("*", SearchOption.AllDirectories).Sum(f => f.Length) : 0;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static void CopyFile(string srcDir, string dstDir, string fileName)
        {
            var srcPath = Path.Combine(srcDir, fileName);
            var dstPath = Path.Combine(dstDir, fileName);
            if (File.Exists(srcPath)) File.Copy(srcPath, dstPath, true);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static void CopyFile(string src, string dst)
        {
            if (File.Exists(src)) File.Copy(src, dst, true);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static string TryCreateDirectory(string path)
        {
            try
            {
                var dir = Directory.CreateDirectory(path);
                return dir.FullName;
            }
            catch
            {
                return null;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
