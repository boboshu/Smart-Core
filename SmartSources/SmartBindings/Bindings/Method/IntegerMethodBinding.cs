using UnityEngine;

namespace Smart.Bindings.Method
{
    [AddComponentMenu("Smart/Bindings/Methods/Integer Method Binding")]
    public class IntegerMethodBinding : MethodBinding<int>
    {
        public void ExecutePlusOne(int param)
        {
            Execute(param + 1);
        }

        public void ExecuteMinusOne(int param)
        {
            Execute(param - 1);
        }
    }
}
