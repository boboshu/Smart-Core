using Smart.Types;
using UnityEngine;
using UnityEngine.Events;

namespace Smart.Bindings.Values
{
    [AddComponentMenu("Smart/Bindings/Values/Boolean Binding")]
    public class BooleanBinding : Binding<bool>
    {
        public enum Kind { None, GameObjectActivator, GameObjectActivatorInvert, Call, TwoCalls }
        public Kind kind;

        public GameObject activatorTarget;
        public UnityEventBoolean call;
        public UnityEvent callPositive;
        public UnityEvent callNegative;

        protected override void Apply(bool value)
        {
            switch (kind)
            {
                case Kind.GameObjectActivator:
                    if (activatorTarget) activatorTarget.SetActive(value);
                    break;

                case Kind.GameObjectActivatorInvert:
                    if (activatorTarget) activatorTarget.SetActive(!value);
                    break;

                case Kind.Call:
                    call.Invoke(value);
                    break;

                case Kind.TwoCalls:
                    if (value) callPositive.Invoke();
                    else callNegative.Invoke();
                    break;
            }
        }

        protected override bool IsEquals(bool v1, bool v2)
        {
            return v1 == v2;
        }
    }
}
