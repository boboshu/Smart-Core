using Smart.Types;
using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Math Helper")]
    public class MathHelper : MonoBehaviour
    {
        public enum OperationType { Add, Sub, Mult, Div }
        public OperationType operationType;
        public float argument;
        public UnityEventFloat onResult = new UnityEventFloat();

        public void Calculate(float arg)
        {
            switch (operationType)
            {
                case OperationType.Add: onResult.Invoke(arg + argument); break;
                case OperationType.Sub: onResult.Invoke(arg - argument); break;
                case OperationType.Mult: onResult.Invoke(arg * argument); break;
                case OperationType.Div: onResult.Invoke(arg / argument); break;
            }
        }
    }
}
