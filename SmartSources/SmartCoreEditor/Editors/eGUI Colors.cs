using UnityEngine;

namespace Smart.Editors
{
    public static partial class eGUI
    {
        public static readonly Color green = Color.green;
        public static readonly Color red = Color.red;
        public static readonly Color blue = Color.blue;
        public static readonly Color yellow = Color.yellow;
        public static readonly Color cyan = Color.cyan;
        public static readonly Color magenta = Color.magenta;
        public static readonly Color gray = Color.gray;
        public static readonly Color white = Color.white;
        public static readonly Color black = Color.black;

        public static readonly Color violet = Color.Lerp(Color.magenta, Color.cyan, 0.5f);
        public static readonly Color azure = Color.Lerp(Color.blue, Color.cyan, 0.5f);
        public static readonly Color lime = Color.Lerp(Color.green, Color.yellow, 0.5f);
        public static readonly Color orange = Color.Lerp(Color.red, Color.yellow, 0.5f);
        public static readonly Color purple = Color.Lerp(Color.blue, Color.magenta, 0.5f);
        public static readonly Color crimson = Color.Lerp(Color.red, Color.magenta, 0.5f);

        public static readonly Color greenLt = Color.Lerp(Color.green, Color.white, 0.5f);
        public static readonly Color redLt = Color.Lerp(Color.red, Color.white, 0.5f);
        public static readonly Color blueLt = Color.Lerp(Color.blue, Color.white, 0.5f);
        public static readonly Color yellowLt = Color.Lerp(Color.yellow, Color.white, 0.5f);
        public static readonly Color cyanLt = Color.Lerp(Color.cyan, Color.white, 0.5f);
        public static readonly Color magentaLt = Color.Lerp(Color.magenta, Color.white, 0.5f);
        public static readonly Color grayLt = Color.Lerp(Color.gray, Color.white, 0.5f);
        public static readonly Color violetLt = Color.Lerp(violet, Color.white, 0.5f);
        public static readonly Color limeLt = Color.Lerp(lime, Color.white, 0.5f);
        public static readonly Color orangeLt = Color.Lerp(orange, Color.white, 0.5f);
        public static readonly Color azureLt = Color.Lerp(azure, Color.white, 0.5f);
        public static readonly Color purpleLt = Color.Lerp(purple, Color.white, 0.5f);
        public static readonly Color crimsonLt = Color.Lerp(crimson, Color.white, 0.5f);

        public static readonly Color greenDk = Color.Lerp(Color.green, Color.black, 0.5f);
        public static readonly Color redDk = Color.Lerp(Color.red, Color.black, 0.5f);
        public static readonly Color blueDk = Color.Lerp(Color.blue, Color.black, 0.5f);
        public static readonly Color yellowDk = Color.Lerp(Color.yellow, Color.black, 0.5f);
        public static readonly Color cyanDk = Color.Lerp(Color.cyan, Color.black, 0.5f);
        public static readonly Color magentaDk = Color.Lerp(Color.magenta, Color.black, 0.5f);
        public static readonly Color grayDk = Color.Lerp(Color.gray, Color.black, 0.5f);
        public static readonly Color violetDk = Color.Lerp(violet, Color.black, 0.5f);
        public static readonly Color limeDk = Color.Lerp(lime, Color.black, 0.5f);
        public static readonly Color orangeDk = Color.Lerp(orange, Color.black, 0.5f);
        public static readonly Color azureDk = Color.Lerp(azure, Color.black, 0.5f);
        public static readonly Color purpleDk = Color.Lerp(purple, Color.black, 0.5f);
        public static readonly Color crimsonDk = Color.Lerp(crimson, Color.black, 0.5f);

        public static Color background;
        public static Color foreground;

        private static Color _bk;
        private static Color _fg;

        private const int MINI_BUTTON_WIDTH = 16;

        public static void BeginColors()
        {
            _bk = GUI.backgroundColor;
            _fg = GUI.contentColor;
        }

        public static void EndColors()
        {
            GUI.backgroundColor = _bk;
            GUI.contentColor = _fg;
        }

        public static void RememberColors()
        {
            background = GUI.backgroundColor;
            foreground = GUI.contentColor;
        }

        public static void SetBGColor(Color clr, float proportion = 0.5f)
        {
            GUI.backgroundColor = Color.Lerp(background, clr, proportion);
        }

        public static void SetFGColor(Color clr, float proportion = 0.5f)
        {
            GUI.contentColor = Color.Lerp(foreground, clr, proportion);
        }

        public static void SetColor(Color clr, float proportion = 0.5f)
        {
            GUI.backgroundColor = Color.Lerp(background, clr, proportion);
            GUI.contentColor = Color.Lerp(foreground, clr, proportion);
        }

        public static void ResetFG()
        {
            GUI.contentColor = foreground;
        }

        public static void ResetBG()
        {
            GUI.backgroundColor = background;
        }

        public static void ResetColors()
        {
            GUI.backgroundColor = background;
            GUI.contentColor = foreground;
        }
    }
}