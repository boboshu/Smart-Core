using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Delay Helper")]
    public class DelayHelper : MonoBehaviour
    {
        [Range(0, 60)] public float seconds = 1;
        public UnityEvent onAfterDelay = new UnityEvent();
        public bool runOnStart = false;

        void Start()
        {
            if (runOnStart) Run();
        }

        public void Run()
        {
            StartCoroutine(WaitForDelay());
        }

        private IEnumerator WaitForDelay()
        {
            yield return new WaitForSeconds(seconds);
            onAfterDelay.Invoke();
        }
    }
}
