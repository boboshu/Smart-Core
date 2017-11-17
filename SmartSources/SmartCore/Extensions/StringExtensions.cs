using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Smart.Extensions
{
    public static class StringExtensions
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public static string CutLength(this string value, int maxLength = 20)
        {
            if (string.IsNullOrEmpty(value)) return "";
            if (value.Length < maxLength) return value;
            return value.Substring(0, maxLength);
        }

        public static string CutDisplayText(this string value, int maxLength = 20)
        {
            if (string.IsNullOrEmpty(value)) return "";
            if (value.Length < maxLength) return value.Replace('\n', ' ');
            return value.Substring(0, maxLength).Replace('\n', ' ');
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static string RemoveVariableValue(this string value)
        {
            return value.IndexOf('=') < 0 ? value : value.Split('=')[0];
        }

        public static string SplitCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            var s = Regex.Replace(value.Replace('_', ' '), @"(\p{Ll})(\P{Ll})", "$1 $2");
            return char.ToUpper(s[0]) + s.Substring(1);
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
                .Select(x => { var i = x.IndexOf(" in "); return i < 0 ? x : x.Remove(i); }).ToArray();
            return string.Join(Environment.NewLine, arr);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static int ToInt(this string text, int defaultValue = 0)
        {
            return int.TryParse(text, out int value) ? value : defaultValue;
        }

        public static float ToFloat(this string text, float defaultValue = 0)
        {
            return float.TryParse(text, out float value) ? value : defaultValue;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static Dictionary<string,string> ToDictionary(this string text)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var item in text.Split('\n'))
            {
                var pos = item.IndexOf('=');
                if (pos > 0 && pos < item.Length)
                    dictionary.Add(item.Substring(0, pos - 1), item.Substring(pos + 1));
            }
            return dictionary;
        }

        public static string ToString<K,V>(this Dictionary<K,V> dictionary)
        {
            var sb = new StringBuilder();
            foreach (var pair in dictionary)
                sb.Append(pair.Key).Append('=').Append(pair.Value).AppendLine();
            return sb.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
