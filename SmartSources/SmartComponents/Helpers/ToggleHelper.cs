using UnityEngine;
using UnityEngine.Events;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Toggle Helper")]
    public class ToggleHelper : MonoBehaviour
    {
        public bool state;
        public UnityEvent onTrue;
        public UnityEvent onFalse;

        public void Toggle()
        {
            state = !state;
            if (state) onTrue.Invoke();
            else onFalse.Invoke();
        }

        public void SetTrue()
        {
            if (state) return;
            state = true;
            onTrue.Invoke();
        }

        public void SetFalse()
        {
            if (!state) return;
            state = false;
            onFalse.Invoke();
        }
 
        public void Set(bool value)
        {
            if (state == value) return;
            state = value;
            if (state) onTrue.Invoke();
            else onFalse.Invoke();
        }
    }
}