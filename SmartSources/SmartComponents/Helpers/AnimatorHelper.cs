using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Animator Helper")]
    public class AnimatorHelper : RedirectableHelper
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public string parameterName;

        public void SetParameterName(string value)
        {
            parameterName = value;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void SetFloat(float value)
        {
            Enum<Animator>(a => a.SetFloat(parameterName, value));
        }

        public void SetBool(bool value)
        {
            Enum<Animator>(a => a.SetBool(parameterName, value));
        }

        public void SetInteger(int value)
        {
            Enum<Animator>(a => a.SetInteger(parameterName, value));
        }

       //--------------------------------------------------------------------------------------------------------------------------
    }
}
