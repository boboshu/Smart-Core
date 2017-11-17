using System.IO;

namespace Smart.Utils
{
    public static class Size
    {
        //--------------------------------------------------------------------------------------------------------------

        public static long GetFileLength(string fileName)
        {
            return File.Exists(fileName) ? new FileInfo(fileName).Length : 0;
        }

        public static string GetFileSize(string fileName)
        {
            return File.Exists(fileName) ? GetMemSize(new FileInfo(fileName).Length) : "";
        }

        public static string GetMemSize(long len)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            var order = 0;
            var divider = 1;
            while ((len / divider) >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                divider *= 1024;
            }
            return $"{len/(float) divider:0.##} {sizes[order]}";
        }

        //--------------------------------------------------------------------------------------------------------------

        public static string GetFileSizeMb(string fileName)
        {
            return GetMemSizeMb(new FileInfo(fileName).Length);
        }

        public static string GetMemSizeMb(long len)
        {
            var size = (len >> 10) / 1024f;
            return string.Format(size < 100 ? "{0:0.#} Mb" : "{0:0} Mb", size);
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}
