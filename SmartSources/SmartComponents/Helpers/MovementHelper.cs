using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Movement Helper")]
    public class MovementHelper : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public Vector3 speed;
        public Vector3[] presets = new Vector3[0];

        void Update()
        {
            transform.position += speed * Time.deltaTime;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void ApplySpeedPreset(int value)
        {
            if (presets.Length < value && value > 0) speed = presets[value];
        }

        public void ClearSpeed()
        {
            speed = Vector3.zero;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void SetSpeedX(float value)
        {
            speed.x = value;
        }

        public void SetSpeedY(float value)
        {
            speed.y = value;
        }

        public void SetSpeedZ(float value)
        {
            speed.z = value;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void SetOnlySpeedX(float value)
        {
            speed = new Vector3(value, 0, 0);
        }

        public void SetOnlySpeedY(float value)
        {
            speed = new Vector3(0, value, 0);
        }

        public void SetOnlySpeedZ(float value)
        {
            speed = new Vector3(0, 0, value);
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
