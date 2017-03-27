using UnityEngine;

namespace Smart.Utils
{
    public static class CoreSettings
    {
        public static bool DrawGizmos
        {
            get { return PlayerPrefs.GetInt("Core_DrawGizmos", 1) == 1; }
            set { PlayerPrefs.SetInt("Core_DrawGizmos", value ? 1 : 0); PlayerPrefs.Save(); }
        }
    }
}
