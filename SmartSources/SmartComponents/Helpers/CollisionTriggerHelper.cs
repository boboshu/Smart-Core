using Smart.Types;
using UnityEngine;

namespace Smart.Helpers
{
    [RequireComponent(typeof(Collider))]
    public class CollisionTriggerHelper : MonoBehaviour
    {
        public GameObject requireGameObject;
        public string requireTag = "Untagged";

        public UnityEventGameObject onEnter = new UnityEventGameObject();
        public UnityEventGameObject onExit = new UnityEventGameObject();

        private bool _isEntered;

        void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (_isEntered) return;

            if (requireGameObject != null)
            {
                var rtr = requireGameObject.transform;
                var ctr = other.transform;
                while (ctr != null)
                {
                    if (rtr == ctr)
                    {
                        onEnter.Invoke(other.gameObject);
                        _isEntered = true;
                        return;
                    }
                    ctr = ctr.transform.parent;
                }
            }
            else if (other.tag == requireTag)
            {
                onEnter.Invoke(other.gameObject);
                _isEntered = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (!_isEntered) return;

            if (requireGameObject != null)
            {
                var rtr = requireGameObject.transform;
                var ctr = other.transform;
                while (ctr != null)
                {
                    if (rtr == ctr)
                    {
                        onExit.Invoke(other.gameObject);
                        _isEntered = false;
                        return;
                    }
                    ctr = ctr.transform.parent;
                }
            }
            else if (other.tag == requireTag)
            {
                onExit.Invoke(other.gameObject);
                _isEntered = false;
            }
        }
    }
}