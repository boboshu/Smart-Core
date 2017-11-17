using UnityEngine;
using UnityEngine.Events;

namespace Smart.StateMachine
{
    public class EventsStateMachineBehaviour : StateMachineBehaviour
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public UnityEvent onStateEnter;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            onStateEnter.Invoke();
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public UnityEvent onStateExit;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            onStateExit.Invoke();
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
