using UnityEngine;

namespace Smart.Extensions
{
    public static class eSettings
    {
        public static int TransformAutoCollapse
        {
            get { return PlayerPrefs.GetInt("Smart_TransformAutoCollapse", 15); }
            set { PlayerPrefs.SetInt("Smart_TransformAutoCollapse", value); PlayerPrefs.Save(); }
        }

        public static bool TransformToolsButtons
        {
            get { return PlayerPrefs.GetInt("Smart_TransformToolsButtons", 1) == 1; }
            set { PlayerPrefs.SetInt("Smart_TransformToolsButtons", value ? 1 : 0); PlayerPrefs.Save(); }
        }

        public static bool TransformExtendedTools
        {
            get { return PlayerPrefs.GetInt("Smart_TransformExtendedTools", 0) == 1; }
            set { PlayerPrefs.SetInt("Smart_TransformExtendedTools", value ? 1 : 0); PlayerPrefs.Save(); }
        }

        public static bool HierarchyExtension
        {
            get { return PlayerPrefs.GetInt("Smart_HierarchyExtension", 1) == 1; }
            set { PlayerPrefs.SetInt("Smart_HierarchyExtension", value ? 1 : 0); PlayerPrefs.Save(); }
        }

        public static bool HierarchyOffsetCheckboxes
        {
            get { return PlayerPrefs.GetInt("Smart_HierarchyOffsetCheckboxes", 0) == 1; }
            set { PlayerPrefs.SetInt("Smart_HierarchyOffsetCheckboxes", value ? 1 : 0); PlayerPrefs.Save(); }
        }

        public static bool HierarchyShowPolyCount
        {
            get { return PlayerPrefs.GetInt("Smart_HierarchyShowPolyCount", 1) == 1; }
            set { PlayerPrefs.SetInt("Smart_HierarchyShowPolyCount", value ? 1 : 0); PlayerPrefs.Save(); }
        }

        public static bool HierarchyShowLayer
        {
            get { return PlayerPrefs.GetInt("Smart_HierarchyShowLayer", 1) == 1; }
            set { PlayerPrefs.SetInt("Smart_HierarchyShowLayer", value ? 1 : 0); PlayerPrefs.Save(); }
        }

        public static bool HierarchyShowTag
        {
            get { return PlayerPrefs.GetInt("Smart_HierarchyShowTag", 1) == 1; }
            set { PlayerPrefs.SetInt("Smart_HierarchyShowTag", value ? 1 : 0); PlayerPrefs.Save(); }
        }

        public static bool EventsExtension
        {
            get { return PlayerPrefs.GetInt("Smart_EventsExtension", 1) == 1; }
            set { PlayerPrefs.SetInt("Smart_EventsExtension", value ? 1 : 0); PlayerPrefs.Save(); }
        }

        public static bool AABBGizmo
        {
            get { return PlayerPrefs.GetInt("Smart_AABBGizmo", 1) == 1; }
            set { PlayerPrefs.SetInt("Smart_AABBGizmo", value ? 1 : 0); PlayerPrefs.Save(); }
        }
    }
}
