using Smart.Editors;
using UnityEditor;
using UnityEngine;

namespace Smart.Toolbars
{
    public class eToolbarMain : eToolbar
    {
        private static eToolbar _window;

        [MenuItem("Tools/Smart Toolbars/Main #&M", false, 1)]
        private static void MainMenuItem()
        {
            if (_window) _window.Close();
            else _window = OpenWindow<eToolbarMain>("Main");
        }

        protected override Color GetColor()
        {
            return eGUI.gray;
        }
    }

    public class eToolbarWhite : eToolbar
    {
        private static eToolbar _window;

        [MenuItem("Tools/Smart Toolbars/White #&W", false, 2)]
        private static void MainMenuItem()
        {
            if (_window) _window.Close();
            else _window = OpenWindow<eToolbarWhite>("White");
        }

        protected override Color GetColor()
        {
            return eGUI.white;
        }
    }

    public class eToolbarYellow : eToolbar
    {
        private static eToolbar _window;

        [MenuItem("Tools/Smart Toolbars/Yellow #&Y", false, 3)]
        private static void MainMenuItem()
        {
            if (_window) _window.Close();
            else _window = OpenWindow<eToolbarYellow>("Yellow");
        }

        protected override Color GetColor()
        {
            return eGUI.yellow;
        }
    }

    public class eToolbarViolet : eToolbar
    {
        private static eToolbar _window;

        //[MenuItem("Tools/Smart Toolbars/Violet #&V", false, 4)]
        [MenuItem("Tools/Smart Toolbars/Violet", false, 100)]
        private static void MainMenuItem()
        {
            if (_window) _window.Close();
            else _window = OpenWindow<eToolbarViolet>("Violet");
        }

        protected override Color GetColor()
        {
            return eGUI.violet;
        }
    }

    public class eToolbarBlack : eToolbar
    {
        [MenuItem("Tools/Smart Toolbars/Black", false, 101)]
        private static void MainMenuItem()
        {
            OpenWindow<eToolbarBlack>("Black");
        }

        protected override Color GetColor()
        {
            return eGUI.black;
        }
    }

    public class eToolbarBlue : eToolbar
    {
        [MenuItem("Tools/Smart Toolbars/Blue", false, 102)]
        private static void MainMenuItem()
        {
            OpenWindow<eToolbarBlue>("Blue");
        }

        protected override Color GetColor()
        {
            return eGUI.azure;
        }
    }

    public class eToolbarGreen : eToolbar
    {
        [MenuItem("Tools/Smart Toolbars/Green", false, 103)]
        private static void MainMenuItem()
        {
            OpenWindow<eToolbarGreen>("Green");
        }

        protected override Color GetColor()
        {
            return eGUI.green;
        }
    }

    public class eToolbarRed : eToolbar
    {
        [MenuItem("Tools/Smart Toolbars/Red", false, 104)]
        private static void MainMenuItem()
        {
            OpenWindow<eToolbarRed>("Red");
        }

        protected override Color GetColor()
        {
            return eGUI.red;
        }
    }
}