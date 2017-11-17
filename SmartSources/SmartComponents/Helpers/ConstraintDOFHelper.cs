using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Constraint DOF Helper")]
    public class ConstraintDOFHelper : MonoBehaviour
    {
        public enum ConstraintType { None, Range, Lock }

        public ConstraintType positionX;
        public float positionX_Min = 0;
        public float positionX_Max = 0;

        public ConstraintType positionY;
        public float positionY_Min = 0;
        public float positionY_Max = 0;

        public ConstraintType positionZ;
        public float positionZ_Min = 0;
        public float positionZ_Max = 0;

        public ConstraintType rotationX;
        public float rotationX_Min = 0;
        public float rotationX_Max = 0;

        public ConstraintType rotationY;
        public float rotationY_Min = 0;
        public float rotationY_Max = 0;

        public ConstraintType rotationZ;
        public float rotationZ_Min = 0;
        public float rotationZ_Max = 0;

        public enum AnglesRangeType { From_0_To_360, From_m180_To_180 }
        public AnglesRangeType anglesRangeType;

        private Vector3 _initialPosition;
        private Vector3 _initialRotation;

        void Start()
        {
            _initialPosition = transform.localPosition;
            _initialRotation = transform.localEulerAngles;
        }

        void Update()
        {
            // Constraint position
            var lp = transform.localPosition;
            var px = ApplyPositionConstraint(lp.x, positionX, positionX_Min, positionX_Max, _initialPosition.x);
            var py = ApplyPositionConstraint(lp.y, positionY, positionY_Min, positionY_Max, _initialPosition.y);
            var pz = ApplyPositionConstraint(lp.z, positionZ, positionZ_Min, positionZ_Max, _initialPosition.z);
            transform.localPosition = new Vector3(px, py, pz);

            // Constraint rotation
            var lr = transform.localEulerAngles;
            var rx = ApplyRotationConstraint(lr.x, rotationX, rotationX_Min, rotationX_Max, _initialRotation.x, anglesRangeType);
            var ry = ApplyRotationConstraint(lr.y, rotationY, rotationY_Min, rotationY_Max, _initialRotation.y, anglesRangeType);
            var rz = ApplyRotationConstraint(lr.z, rotationZ, rotationZ_Min, rotationZ_Max, _initialRotation.z, anglesRangeType);
            transform.localRotation = Quaternion.Euler(rx, ry, rz);
        }

        private static float ApplyPositionConstraint(float value, ConstraintType constraintType, float min, float max, float initial)
        {
            switch (constraintType)
            {
                default: return value;
                case ConstraintType.Lock: return initial;
                case ConstraintType.Range: return Mathf.Clamp(value, min, max);
            }
        }

        private static float ApplyRotationConstraint(float value, ConstraintType constraintType, float min, float max, float initial, AnglesRangeType anglesRangeType)
        {
            switch (constraintType)
            {
                default: return value;
                case ConstraintType.Lock: return initial;
                case ConstraintType.Range:
                    switch (anglesRangeType)
                    {
                        case AnglesRangeType.From_0_To_360:
                            if (value < 0) value += 360;
                            if (value > 360) value -= 360;
                            break;

                        case AnglesRangeType.From_m180_To_180:
                            if (value > 180) value -= 360;
                            if (value < -180) value += 360;
                            break;
                    }                    
                    return Mathf.Clamp(value, min, max);
            }
        }
    }
}
