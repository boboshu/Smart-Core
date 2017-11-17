using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Spring Joint Helper")]
    public class SpringJointHelper : JointHelper
    {
        //-------------------------------------------------------------------------------

        public void SetSpringSpring(float value)
        {
            Enum<SpringJoint>(j => j.spring = value);
        }

        public void SetSpringDamper(float value)
        {
            Enum<SpringJoint>(j => j.damper = value);
        }

        public void SetSpringTolerance(float value)
        {
            Enum<SpringJoint>(j => j.tolerance = value);
        }

        public void SetSpringMinDistance(float value)
        {
            Enum<SpringJoint>(j => j.minDistance = value);
        }

        public void SetSpringMaxDistance(float value)
        {
            Enum<SpringJoint>(j => j.maxDistance = value);
        }

        //-------------------------------------------------------------------------------
    }
}
