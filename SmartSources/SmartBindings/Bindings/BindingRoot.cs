using UnityEngine;

namespace Smart.Bindings
{
    [AddComponentMenu("Smart/Bindings/Root Binding")]
    public class BindingRoot : MonoBehaviour
    {
        public Object sourceObject;

        public void SetSource(Object obj)
        {
            sourceObject = obj;
        }

        public void SetSource(MonoBehaviour obj)
        {
            sourceObject = obj;
        }

        public void SetSource(GameObject obj)
        {
            sourceObject = obj;
        }

        public void SetSource(Component obj)
        {
            sourceObject = obj;
        }
    }
}
