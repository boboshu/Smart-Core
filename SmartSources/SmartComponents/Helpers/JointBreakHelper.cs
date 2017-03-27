using System;
using UnityEngine;
using UnityEngine.Events;

namespace Smart.Helpers
{
    [RequireComponent(typeof(Joint))]
    public class JointBreakHelper : MonoBehaviour
    {
        [HideInInspector, NonSerialized]
        public Action onBreakAction;
        public UnityEvent onBreakEvent = new UnityEvent();
        public bool writeToLog;

        void OnJointBreak(float breakForce)
        {
            if (onBreakAction != null) onBreakAction();
            onBreakEvent.Invoke();
            if (writeToLog) Debug.Log("Joint Break on " + name + " with force " + breakForce);
        }
    }
}
