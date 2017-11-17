using System;
using Smart.Extensions;
using UnityEngine;

namespace Smart.Helpers
{
    public abstract class RedirectableHelper : MonoBehaviour
    {
        //-------------------------------------------------------------------------------

        public GameObject redirect;

        public GameObject TargetGameObject => redirect == null ? gameObject: redirect; // can`t use ?? - BUG of compiling in Unity: The variable redirect of CommandHelper has not been assigned.

        //-------------------------------------------------------------------------------

        public bool enumInChildren;

        protected void Enum<T>(Action<T> action) where T : Component
        {
            if (enumInChildren)
            {
                TargetGameObject.GetComponentsInChildren<T>(true).Do(action.Invoke);
            }
            else
            {
                var cmp = TargetGameObject.GetComponent<T>();
                if (cmp) action.Invoke(cmp);
            }
        }

        //-------------------------------------------------------------------------------
    }
}
