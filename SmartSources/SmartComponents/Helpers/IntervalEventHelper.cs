using Smart.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Interval Event Helper")]
    public class IntervalEventHelper : MonoBehaviour
    {
        [Range(0, 60)] public float interval = 1;
        public UnityEvent onInterval = new UnityEvent();

        void OnEnable()
        {
            UpdatesManager.Subscribe(onInterval.Invoke, interval);
        }

        void OnDisable()
        {
            UpdatesManager.UnSubscribe(onInterval.Invoke);
        }

        public void SetInterval(float newInterval)
        {
            interval = newInterval;
            UpdateInterval();
        }

        public void UpdateInterval()
        {
            if (enabled)
            {
                UpdatesManager.UnSubscribe(onInterval.Invoke);
                UpdatesManager.Subscribe(onInterval.Invoke, interval);
            }
        }       
    }
}
