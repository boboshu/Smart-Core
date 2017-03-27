using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Smart.Extensions
{
    public static class StringExtensions
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public static string SplitCamelCase(this string value)
        {
            return Regex.Replace(Regex.Replace(value, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        //--------------------------------------------------------------------------------------------------------------------------

        /// <summary> Jenkins hash </summary>
        public static uint CalcHash32(this string value)
        {
            var i = 0;
            var len = value.Length;
            var hash = 0u;
            while (i != len)
            {
                hash += value[i++];
                hash += hash << 10;
                hash ^= hash >> 10;
            }
            hash += hash << 3;
            hash ^= hash >> 11;
            hash += hash << 15;
            return hash;
        }

        /// <summary> Knuth hash </summary>
        public static ulong CalcHash64(this string value)
        {
            var len = value.Length;
            var hash = 3074457345618258791ul;
            for (var i = 0; i < len; i++)
            {
                hash += value[i];
                hash *= 3074457345618258799ul;
            }
            return hash;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static string MakeStackTraceShorter(this string stackTrace, int skipCount, int maxCount = 10)
        {
            var arr = stackTrace.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            arr = arr.Skip(skipCount).Select(x => x.Trim()).Take(maxCount).ToArray();
            return string.Join(Environment.NewLine, arr);
        }

        public static string MakeStackTraceShorter(this string stackTrace, string removeStartsWith, int skipCount, int maxCount = 10)
        {
            var arr = stackTrace.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            arr = arr.Skip(skipCount).Select(x => x.Trim().Replace("at ", "")).Where(x => !x.StartsWith(removeStartsWith)).Take(maxCount)
                .Select(x => { var i = x.IndexOf(" in "); return (i < 0 ? x : x.Remove(i)); }).ToArray();
            return string.Join(Environment.NewLine, arr);
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
