using System.Text;
using Smart.Types;
using UnityEngine;

namespace Smart.Extensions
{
    public static class ColorExtensions
    {
        //--------------------------------------------------------------------------------------------------------------------------
        
        public static Color ChangeAlpha(this Color color, float newAlpha)
        {
            return new Color(color.r, color.g, color.b, newAlpha);
        }

        public static Color MultiplyRGB(this Color color, float multiplier)
        {
            return new Color(color.r * multiplier, color.g * multiplier, color.b * multiplier, color.a);
        }

        public static Color MultiplyAlpha(this Color color, float multiplier)
        {
            return new Color(color.r, color.g, color.b, color.a * multiplier);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static Color InvertAndKeepAlpha(this Color value)
        {
            return new Color(1 - value.r, 1 - value.g, 1 - value.b, value.a);
        }

        public static Color InvertAndSetAlpha(this Color value, float newAlpha)
        {
            return new Color(1 - value.r, 1 - value.g, 1 - value.b, newAlpha);
        }

        public static Color InvertWithAlpha(this Color value)
        {
            return new Color(1 - value.r, 1 - value.g, 1 - value.b, 1 - value.a);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static Color ToColor(this int value)
        {
            return new Color(
                (byte)value / 255f,
                (byte)(value >> 8) / 255f,
                (byte)(value >> 16) / 255f,
                (byte)(value >> 24) / 255f);
        }

        public static Color ToColor(this uint value)
        {
            return new Color(
                (byte)value / 255f,
                (byte)(value >> 8) / 255f,
                (byte)(value >> 16) / 255f,
                (byte)(value >> 24) / 255f);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static int ToInt(this Color color)
        {
            var r = (byte)(Mathf.Clamp01(color.r) * 255f + 0.5f);
            var g = (byte)(Mathf.Clamp01(color.g) * 255f + 0.5f);
            var b = (byte)(Mathf.Clamp01(color.b) * 255f + 0.5f);
            var a = (byte)(Mathf.Clamp01(color.a) * 255f + 0.5f);
            return r | (g << 8) | (b << 16) | (a << 24);
        }

        public static uint ToUInt(this Color color)
        {
            var r = (uint)(Mathf.Clamp01(color.r) * 255f + 0.5f);
            var g = (uint)(Mathf.Clamp01(color.g) * 255f + 0.5f);
            var b = (uint)(Mathf.Clamp01(color.b) * 255f + 0.5f);
            var a = (uint)(Mathf.Clamp01(color.a) * 255f + 0.5f);
            return r | (g << 8) | (b << 16) | (a << 24);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static string ToHex(this Color color)
        {
            return ColorUtility.ToHtmlStringRGBA(color);
        }

        public static Color AsHexColor(this string hex)
        {
            Color clr;
            if (ColorUtility.TryParseHtmlString(hex, out clr)) return clr;
            if (ColorUtility.TryParseHtmlString('#' + hex, out clr)) return clr;
            return Color.black;
        }

        public static Color AsHexColor(this string hex, Color defColor)
        {
            Color clr;
            if (ColorUtility.TryParseHtmlString(hex, out clr)) return clr;
            if (ColorUtility.TryParseHtmlString('#' + hex, out clr)) return clr;
            return defColor;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static string AsColorTextBlock(this Color color, int length = 10)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < length; i++)
                sb.Append(color.a * length > i ? '█' : '▓');//▓▒░

            return sb.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static HSBColor ToHSBColor(this Color color)
        {
            return HSBColor.FromRGBColor(color);
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}