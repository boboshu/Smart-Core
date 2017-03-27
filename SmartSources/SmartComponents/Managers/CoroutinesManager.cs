using System.Collections;
using Smart.Types;
using UnityEngine;

namespace Smart.Managers
{
    [ClearTransform]
    [AddComponentMenu("Smart/Managers/Coroutines Manager")]
    public class CoroutinesManager : MonoBehaviour
    {
        public bool dontDestroyOnLoad = true;
        private static CoroutinesManager _instance;

        void Awake()
        {
            _instance = this;
            if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        }

        void OnDestroy()
        {
            _instance = null;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        public static void Start(IEnumerator routine)
        {
            if (_instance == null) return;
            _instance.StartCoroutine(routine);
        }

        public static void Stop(IEnumerator routine)
        {
            if (_instance == null) return;
            _instance.StopCoroutine(routine);
        }

        public static void StopAll()
        {
            _instance.StopAllCoroutines();
        }
    }
}
