using UnityEditor;
using UnityEngine;

namespace Smart.Extensions
{
    public static class eSceneViews
    {
        private class View
        {
            public Vector3 position;
            public Quaternion rotation;
        }

        private static readonly View[] _views = new View[10];

        //----------------------------------------------------------------------------------------------------------------------------------

        public static void ActivateView(int index)
        {
            if (index < 0 || index > 9) return;
            var view = _views[index];
            if (view == null) return;

            if (SceneView.lastActiveSceneView == null) return;

            SceneView.lastActiveSceneView.pivot = view.position;
            SceneView.lastActiveSceneView.rotation = view.rotation;
            SceneView.lastActiveSceneView.Repaint();
        }

        public static void RememberView(int index)
        {
            if (index < 0 || index > 9) return;
            var view = _views[index] ?? (_views[index] = new View());

            if (SceneView.lastActiveSceneView == null) return;

            view.position = SceneView.lastActiveSceneView.pivot;
            view.rotation = SceneView.lastActiveSceneView.rotation;
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        [MenuItem("Tools/Smart Views/Activate View 1 &1", false, 1)]
        private static void ActivateView1()
        {
            ActivateView(1);
        }

        [MenuItem("Tools/Smart Views/Remember View 1 #&1", false, 101)]
        private static void RememberView1()
        {
            RememberView(1);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        [MenuItem("Tools/Smart Views/Activate View 2 &2", false, 2)]
        private static void ActivateView2()
        {
            ActivateView(2);
        }

        [MenuItem("Tools/Smart Views/Remember View 2 #&2", false, 102)]
        private static void RememberView2()
        {
            RememberView(2);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        [MenuItem("Tools/Smart Views/Activate View 3 &3", false, 3)]
        private static void ActivateView3()
        {
            ActivateView(3);
        }

        [MenuItem("Tools/Smart Views/Remember View 3 #&3", false, 103)]
        private static void RememberView3()
        {
            RememberView(3);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        [MenuItem("Tools/Smart Views/Activate View 4 &4", false, 4)]
        private static void ActivateView4()
        {
            ActivateView(4);
        }

        [MenuItem("Tools/Smart Views/Remember View 4 #&4", false, 104)]
        private static void RememberView4()
        {
            RememberView(4);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        [MenuItem("Tools/Smart Views/Activate View 5 &5", false, 5)]
        private static void ActivateView5()
        {
            ActivateView(5);
        }

        [MenuItem("Tools/Smart Views/Remember View 5 #&5", false, 105)]
        private static void RememberView5()
        {
            RememberView(5);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        [MenuItem("Tools/Smart Views/Activate View 6 &6", false, 6)]
        private static void ActivateView6()
        {
            ActivateView(6);
        }

        [MenuItem("Tools/Smart Views/Remember View 6 #&6", false, 106)]
        private static void RememberView6()
        {
            RememberView(6);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        [MenuItem("Tools/Smart Views/Activate View 7 &7", false, 7)]
        private static void ActivateView7()
        {
            ActivateView(7);
        }

        [MenuItem("Tools/Smart Views/Remember View 7 #&7", false, 107)]
        private static void RememberView7()
        {
            RememberView(7);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        [MenuItem("Tools/Smart Views/Activate View 8 &8", false, 8)]
        private static void ActivateView8()
        {
            ActivateView(8);
        }

        [MenuItem("Tools/Smart Views/Remember View 8 #&8", false, 108)]
        private static void RememberView8()
        {
            RememberView(8);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        [MenuItem("Tools/Smart Views/Activate View 9 &9", false, 9)]
        private static void ActivateView9()
        {
            ActivateView(9);
        }

        [MenuItem("Tools/Smart Views/Remember View 9 #&9", false, 109)]
        private static void RememberView9()
        {
            RememberView(9);
        }

        //----------------------------------------------------------------------------------------------------------------------------------
    }
}
