using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Smart.Extensions
{
    /// <summary>
    /// Class for making ContainerWindows and EditorWindows fullscreen
    /// </summary>
    public class eFullscreen
    {        
        private const string TEMP_LAYOUT = "Temp/TemporaryLayout.dwlt";
        private const BindingFlags FULL_BINDING = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        private static readonly Type containerWindowType;
        private static readonly eFullscreen gameView;
        private static readonly eFullscreen sceneView;
        private static readonly eFullscreen inspectorView;
        private static readonly eFullscreen projectView;
        private static readonly eFullscreen assetStoreView;
        //        private static readonly eFullscreen frameDebuggerView;
        //        private static readonly eFullscreen animationView;
        private static readonly eFullscreen profilerView;
        private static readonly eFullscreen preferencesView;
        private static readonly eFullscreen fullscreenView;
        private static readonly eFullscreen consoleView;

        private readonly MethodInfo closeMethod;
        private readonly MethodInfo openMethod;
        private readonly PropertyInfo positionProperty;

        /// <summary>
        /// The name of the fullscreen window
        /// </summary>
        public readonly string name;

        /// <summary>
        /// The type of the fullscreen window
        /// </summary>
        public readonly Type type;

        /// <summary>
        /// Static constructor
        /// </summary>
        static eFullscreen()
        {
            containerWindowType = typeof(Editor).Assembly.GetType("UnityEditor.ContainerWindow");

            gameView = new eFullscreen(typeof(Editor).Assembly.GetType("UnityEditor.GameView"));
            sceneView = new eFullscreen(typeof(SceneView));
            inspectorView = new eFullscreen(typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow"));
            projectView = new eFullscreen(typeof(Editor).Assembly.GetType("UnityEditor.ProjectBrowser"));
            assetStoreView = new eFullscreen(typeof(Editor).Assembly.GetType("UnityEditor.AssetStoreWindow"));
//            animationView = new eFullscreen(typeof(Editor).Assembly.GetType("UnityEditor.AnimationWindow"));
//            frameDebuggerView = new eFullscreen(typeof(Editor).Assembly.GetType("UnityEditor.FrameDebuggerWindow"));
            profilerView = new eFullscreen(typeof(Editor).Assembly.GetType("UnityEditor.ProfilerWindow"));
            preferencesView = new eFullscreen(typeof(Editor).Assembly.GetType("UnityEditor.PreferencesWindow"));
            fullscreenView = new eFullscreen(containerWindowType);
            consoleView = new eFullscreen(typeof(Editor).Assembly.GetType("UnityEditor.ConsoleWindow"));
         
            //var sb = new StringBuilder();
            //foreach (var type in typeof(Editor).Assembly.GetTypes().Where(t => t.Name.Contains("Collab")))
            //foreach (var type in typeof(Editor).Assembly.GetTypes().Where(t => typeof(EditorWindow).IsAssignableFrom(t)))
            //    sb.AppendLine(type.FullName);
            //File.WriteAllText(@"C:\1\Editors.txt", sb.ToString());
        }

        /// <summary>
        /// Class for making ContainerWindows or EditorWindows fullscreen 
        /// </summary>
        /// <param name="type">The type of the window that will be in fullscreen</param>
        public eFullscreen(Type type)
        {
            if (!IsValidType(type))
                throw new ArgumentException("The type must be inherited from UnityEditor.EditorWindow or UnityEditor.ContainerWindow");

            this.type = type;

            name = string.Format("{0}_FullScreen", type);
            closeMethod = type.GetMethod("Close", FULL_BINDING);
            openMethod = type.GetMethod("ShowPopup", FULL_BINDING);
            positionProperty = type.GetProperty("position", FULL_BINDING);
        }

        /// <summary>
        /// Is this window currently in fullscreen mode?
        /// </summary>
        public bool isOppened => currentOppened;

        /// <summary>
        /// The instance of the window in fullscreen, null if not in fullscreen
        /// </summary>
        public Object currentOppened => (from obj in Resources.FindObjectsOfTypeAll(type)
            where obj && obj.name == name
            select obj).FirstOrDefault();

        /// <summary>
        /// Show the window in fullscreen
        /// </summary>
        public void Show()
        {
            Show(null);
        }

        /// <summary>
        /// Show the window in fullscreen
        /// </summary>
        /// <param name="reference">The window that will be cloned, must be the inherited from the type passed in the constructor</param>
        public void Show(Object reference)
        {
            if (reference && IsValidType(reference.GetType()))
                reference = Object.Instantiate(reference);
            else
                reference = null;

            var window = reference ?? currentOppened ?? ScriptableObject.CreateInstance(type);
            var selection = Vector2.zero;

            if (EditorWindow.mouseOverWindow)
                selection = EditorWindow.mouseOverWindow.position.center;

            window.name = name;
            openMethod.Invoke(window, null);
            positionProperty.SetValue(window, InternalEditorUtility.GetBoundsOfDesktopAtPoint(selection), null);
        }

        /// <summary>
        /// Close the fullscreen window
        /// </summary>
        public void Close()
        {
            if (currentOppened)
                closeMethod.Invoke(currentOppened, null);
        }

        /// <summary>
        /// Open the window if it's closed, or close it, if it's already open
        /// </summary>
        public void Toggle()
        {
            if (!isOppened)
                Show();
            else
                Close();
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        [MenuItem("Tools/Smart/Game View _F3", false, 103)]
        public static void ToggleGameView()
        {
            inspectorView.Close();
            projectView.Close();
//            animationView.Close();
//            frameDebuggerView.Close();
            profilerView.Close();
            preferencesView.Close();
            assetStoreView.Close();
            consoleView.Close();
            sceneView.Close();
            gameView.Toggle();
        }

        [MenuItem("Tools/Smart/Scene View _F4", false, 104)]
        public static void ToggleSceneView()
        {
            inspectorView.Close();
            projectView.Close();
//            animationView.Close();
//            frameDebuggerView.Close();
            profilerView.Close();
            preferencesView.Close();
            assetStoreView.Close();
            consoleView.Close();
            gameView.Close();
            sceneView.Toggle();
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        [MenuItem("Tools/Smart/Inspector View _F5", false, 105)]
        public static void ToggleInspectorView()
        {
            projectView.Close();
//            animationView.Close();
//            frameDebuggerView.Close();
            profilerView.Close();
            preferencesView.Close();
            assetStoreView.Close();
            consoleView.Close();
            sceneView.Close();
            gameView.Close();
            inspectorView.Toggle();
        }

        [MenuItem("Tools/Smart/Project View _F6", false, 105)]
        public static void ToggleSomeView()
        {
            inspectorView.Close();
//            frameDebuggerView.Close();
//            animationView.Close();
            profilerView.Close();
            preferencesView.Close();
            consoleView.Close();
            gameView.Close();
            sceneView.Close();
            assetStoreView.Close();
            projectView.Toggle();
        }

        [MenuItem("Tools/Smart/AssetStore View _F7", false, 107)]
        public static void ToggleAssetStoreView()
        {
            inspectorView.Close();
            projectView.Close();
//            animationView.Close();
//            frameDebuggerView.Close();
            profilerView.Close();
            preferencesView.Close();            
            consoleView.Close();
            gameView.Close();
            sceneView.Close();
            assetStoreView.Toggle();
        }

//        [MenuItem("Tools/Smart/Animation View _F6", false, 106)]
//        public static void TogglePreviewView()
//        {
//            inspectorView.Close();
//            projectView.Close();
//            profilerView.Close();
//            preferencesView.Close();
//            consoleView.Close();
//            gameView.Close();
//            sceneView.Close();
//            assetStoreView.Close();
//            frameDebuggerView.Close();
//            animationView.Toggle();
//        }

//        [MenuItem("Tools/Smart/FrameDebugger View _F7", false, 107)]
//        public static void ToggleFrameDebuggerView()
//        {
//            inspectorView.Close();
//            projectView.Close();
//            profilerView.Close();
//            preferencesView.Close();
//            assetStoreView.Close();
//            consoleView.Close();
//            sceneView.Close();
//            gameView.Close();
//            animationView.Close();
//            frameDebuggerView.Toggle();
//        }

        [MenuItem("Tools/Smart/Profiler View _F8", false, 108)]
        public static void ToggleProfilerView()
        {
            inspectorView.Close();
            projectView.Close();
//            animationView.Close();
//            frameDebuggerView.Close();
            preferencesView.Close();
            assetStoreView.Close();
            consoleView.Close();
            sceneView.Close();
            gameView.Close();
            profilerView.Toggle();
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        [MenuItem("Tools/Smart/Run _F9", false, 109)]
        private static void Run()
        {
            EditorApplication.isPlaying = !EditorApplication.isPlaying;
        }


        [MenuItem("Tools/Smart/Preferences View _F10", false, 110)]
        public static void TogglePreferencesView()
        {
            inspectorView.Close();
            projectView.Close();
//            animationView.Close();
//            frameDebuggerView.Close();
            profilerView.Close();
            assetStoreView.Close();
            consoleView.Close();
            sceneView.Close();
            gameView.Close();
            preferencesView.Toggle();
        }

        [MenuItem("Tools/Smart/Fullscreen _F11", false, 111)]
        public static void ToggleFullscreenView()
        {
            inspectorView.Close();
            projectView.Close();
//            animationView.Close();
//            frameDebuggerView.Close();
            profilerView.Close();
            preferencesView.Close();
            assetStoreView.Close();
            gameView.Close();
            sceneView.Close();
            consoleView.Close();            

            if (!fullscreenView.isOppened)
            {
                SaveLayout();
                fullscreenView.Show(GetMainView());
            }
            else
            {
                fullscreenView.Close();
                LoadLayout();
            }

            InternalEditorUtility.RepaintAllViews();
        }

        [MenuItem("Tools/Smart/Console _F12", false, 112)]
        public static void ToggleConsole()
        {
            inspectorView.Close();
            projectView.Close();
//            animationView.Close();
//            frameDebuggerView.Close();
            profilerView.Close();
            preferencesView.Close();
            assetStoreView.Close();
            gameView.Close();
            sceneView.Close();
            consoleView.Toggle();
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Check if its a valid type for fullscreen
        /// </summary>
        /// <param name="type">The type to check</param>
        private static bool IsValidType(Type type)
        {
            if (type == containerWindowType || type == typeof(EditorWindow))
                return true;
            if (type.IsSubclassOf(containerWindowType) || type.IsSubclassOf(typeof(EditorWindow)))
                return true;
            return false;
        }

        /// <summary>
        /// Get the MainView window
        /// </summary>
        /// <returns></returns>
        private static Object GetMainView()
        {
            var containers = Resources.FindObjectsOfTypeAll(containerWindowType);
            var showMode = containerWindowType.GetProperty("showMode", FULL_BINDING);

            for (int i = 0; i < containers.Length; i++)
                if (containers[i] && (int)showMode.GetValue(containers[i], null) == 4)
                    return containers[i];

            throw new NullReferenceException("Couldn't find main view");
        }

        /// <summary>
        /// Save the current layout, so we can restore it after closing the MainView
        /// </summary>
        private static void SaveLayout()
        {
            var windowLayoutType = typeof(Editor).Assembly.GetType("UnityEditor.WindowLayout");
            var saveLayoutMethod = windowLayoutType.GetMethod("SaveWindowLayout", FULL_BINDING);

            saveLayoutMethod.Invoke(null, new object[] { TEMP_LAYOUT });
        }

        /// <summary>
        /// Restore the layout saved before the MainView be in the fullscreen
        /// </summary>
        private static void LoadLayout()
        {
            var windowLayoutType = typeof(Editor).Assembly.GetType("UnityEditor.WindowLayout");
            var loadMethod = windowLayoutType.GetMethod("LoadWindowLayout", FULL_BINDING);

            loadMethod.Invoke(null, new object[] { TEMP_LAYOUT, false });
        }

        /// <summary>
        /// Check if the window is not null and if it's oppened
        /// </summary>
        public static implicit operator bool(eFullscreen window)
        {
            if (window == null)
                return false;
            return window.isOppened;
        }
    }
}