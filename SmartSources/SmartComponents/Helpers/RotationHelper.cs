using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Rotation Helper")]
    public class RotationHelper : MonoBehaviour
    {
        public Vector3 speed;

        void Update()
        {
            transform.Rotate(speed * Time.deltaTime);
        }

        public void SetSpedX(float value)
        {
            speed.x = value;
        }

        public void SetSpedY(float value)
        {
            speed.y = value;
        }

        public void SetSpedZ(float value)
        {
            speed.z = value;
        }
    }
}
