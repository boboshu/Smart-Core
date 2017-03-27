using UnityEngine;
using UnityEngine.Events;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Events Helper")]
    public class EventsHelper : MonoBehaviour
    {
        public UnityEvent onEnable = new UnityEvent();
        public UnityEvent onDisable = new UnityEvent();
        public UnityEvent onStart = new UnityEvent();
        public UnityEvent onDestroy = new UnityEvent();

        void OnEnable()
        {
            onEnable.Invoke();
        }

        void OnDisable()
        {
            onDisable.Invoke();
        }

        void Start()
        {
            onStart.Invoke();
        }

        void OnDestroy()
        {
            onDestroy.Invoke();
        }
    }
}
