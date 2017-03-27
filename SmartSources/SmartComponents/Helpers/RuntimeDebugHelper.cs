using UnityEngine;
using UnityEngine.Events;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Runtime Debug Helper")]
    public class RuntimeDebugHelper : MonoBehaviour
    {
        public UnityEvent onExecute = new UnityEvent();
    }
}
