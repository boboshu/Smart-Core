using UnityEngine;
using UnityEngine.Events;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Select Option Helper")]
    public class SelectOptionHelper : MonoBehaviour
    {
        public UnityEvent[] options = new UnityEvent[0];

        public void Execute(int index)
        {
            if (index >= 0 && index < options.Length)
                options[index].Invoke();
        }
    }
}
