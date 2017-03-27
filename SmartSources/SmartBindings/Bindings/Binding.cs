using UnityEngine;

namespace Smart.Bindings
{
    public abstract class Binding : MonoBehaviour
    {
        public enum UpdatesPerSecond { Half, One, Two, Five, Ten, Twenty }

        public Object sourceObject;
        public string sourceMember;
        public bool useBindingRoot;

        public abstract void UpdateBinding(bool forced);
    }
}
