using UnityEngine;

namespace Smart.Helpers
{
    public class FollowCameraHelper : MonoBehaviour
    {
        public bool syncPosition = true;
        public bool syncRotation = true;

        private Transform _cameraTransform;

        void Update()
        {
            if (_cameraTransform == null)
            {
                var cam = Camera.main;
                if (cam == null) return;
                _cameraTransform = cam.transform;
            }

            if (syncPosition) transform.position = _cameraTransform.position;
            if (syncRotation) transform.rotation = _cameraTransform.rotation;
        }
    }
}
