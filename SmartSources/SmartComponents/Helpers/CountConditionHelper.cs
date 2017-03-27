using UnityEngine;
using UnityEngine.Events;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Count Condition Helper")]
    public class CountConditionHelper : MonoBehaviour
    {
        public enum ConditionType { GreaterOrEqual, LessOrEqual, Greater, Less, Equal }

        public int counter;
        public ConditionType condition;
        public int argument = 3;
        public UnityEvent onConditionTrue = new UnityEvent();
        public UnityEvent onConditionFalse = new UnityEvent();
        
        private bool _lastConditionResult;

        public void Inc()
        {
            Set(counter + 1);
        }

        public void Inc(int value)
        {
            Set(counter + value);
        }

        public void Dec()
        {
            Set(counter - 1);
        }

        public void Dec(int value)
        {
            Set(counter - value);
        }

        public void Set(int value)
        {
            var result = false;
            counter = value;

            switch (condition)
            {
                case ConditionType.Equal: result = (counter == argument); break;
                case ConditionType.Greater: result = (counter > argument); break;
                case ConditionType.Less: result = (counter > argument); break;
                case ConditionType.GreaterOrEqual: result = (counter >= argument); break;
                case ConditionType.LessOrEqual: result = (counter >= argument); break;
            }

            if (_lastConditionResult == result) return;
            _lastConditionResult = result;
            
            if (result) onConditionTrue.Invoke();
            else onConditionFalse.Invoke();
        }
    }
}
