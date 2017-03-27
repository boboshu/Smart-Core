using UnityEngine;

namespace Smart.Helpers
{
    public abstract class RedirectableHelper : MonoBehaviour
    {
        //-------------------------------------------------------------------------------

        public GameObject redirect;

        public GameObject TargetGameObject => redirect == null ? gameObject: redirect; // can`t use ?? - BUG of compiling in Unity: The variable redirect of CommandHelper has not been assigned.

        //-------------------------------------------------------------------------------
    }
}
