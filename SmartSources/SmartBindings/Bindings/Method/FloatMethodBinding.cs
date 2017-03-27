using UnityEngine;

namespace Smart.Bindings.Method
{
    [AddComponentMenu("Smart/Bindings/Methods/Float Method Binding")]
    public class FloatMethodBinding : MethodBinding<float>
    {
        public void ExecutePlusOne(float param)
        {
            Execute(param + 1);
        }

        public void ExecuteMinusOne(float param)
        {
            Execute(param - 1);
        }
    }
}
