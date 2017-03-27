using Smart.Types;
using UnityEngine;

namespace Smart.Helpers
{
    [RequireComponent(typeof(Collider))]
    public class CollisionTriggerHelper : MonoBehaviour
    {
        public string requireTag = "Untagged";
        public UnityEventGameObject onEnter = new UnityEventGameObject();
        public UnityEventGameObject onExit = new UnityEventGameObject();

        private bool _isPlayerIn;

        void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (_isPlayerIn) return;

            if (other.tag == requireTag)
            {
                onEnter.Invoke(other.gameObject);
                _isPlayerIn = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (!_isPlayerIn) return;

            if (other.tag == requireTag)
            {
                onExit.Invoke(other.gameObject);
                _isPlayerIn = false;
            }
        }
    }
}