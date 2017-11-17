using System;
using System.Linq;
using Smart.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Select Interval Helper")]
    public class SelectIntervalHelper : MonoBehaviour
    {
        [Serializable]
        public class Interval
        {
            public float minimum;
            public float maximum;
            public UnityEvent onEvent = new UnityEvent();
            [NonSerialized] public bool isActive;

            public bool InInterval(float value)
            {
                return minimum <= value && value <= maximum;
            }
        }

        public Interval[] intervals = new Interval[0];
        public bool repeatEvents;

        public void Execute(float value)
        {
            if (repeatEvents)
            {
                intervals.Where(i => i.InInterval(value)).Do(i => i.onEvent.Invoke());
            }
            else
            {
                intervals.Do(i =>
                {
                    var a = i.InInterval(value);
                    if (i.isActive == a) return;
                    i.isActive = a;
                    if (a) i.onEvent.Invoke();
                });
            }
        }
    }
}
