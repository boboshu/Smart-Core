using System;
using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Hinge Joint Helper")]
    public class HingeJointHelper : JointHelper
    {

        //-------------------------------------------------------------------------------

        private static JointMotor _jointMotor;
        private static JointSpring _jointSpring;
        private static JointLimits _jointLimits;

        //-------------------------------------------------------------------------------

        private void Motor(Action action)
        {
            Enum<HingeJoint>(j => { _jointMotor = j.motor; action.Invoke(); j.motor = _jointMotor; });
        }

        private void Spring(Action action)
        {
            Enum<HingeJoint>(j => { _jointSpring = j.spring; action.Invoke(); j.spring = _jointSpring; });
        }

        private void Limits(Action action)
        {
            Enum<HingeJoint>(j => { _jointLimits = j.limits; action.Invoke(); j.limits = _jointLimits; });
        }

        //-------------------------------------------------------------------------------

        public void SetMotorTargetVelocity(float value)
        {
            Motor(() => _jointMotor.targetVelocity = value);
        }

        public void SetMotorTargetForce(float value)
        {
            Motor(() => _jointMotor.force = value);
        }

        public void SetMotorFreeSpin(bool value)
        {
            Motor(() => _jointMotor.freeSpin = value);
        }

        public void SetSpringSpring(float value)
        {
            Spring(() => _jointSpring.spring = value);
        }

        public void SetSpringDamper(float value)
        {
            Spring(() => _jointSpring.damper = value);
        }

        public void SetSpringTargetPosition(float value)
        {
            Spring(() => _jointSpring.targetPosition = value);
        }

        public void SetLimitsMax(float value)
        {
            Limits(() => _jointLimits.max = value);
        }

        public void SetLimitsMin(float value)
        {
            Limits(() => _jointLimits.min = value);
        }

        public void SetLimitsContactDistance(float value)
        {
            Limits(() => _jointLimits.contactDistance = value);
        }

        public void SetLimitsBounciness(float value)
        {
            Limits(() => _jointLimits.bounciness = value);
        }

        public void SetLimitsBounceMinVelocity(float value)
        {
            Limits(() => _jointLimits.bounceMinVelocity = value);
        }

        //-------------------------------------------------------------------------------

        public void SetUseMotor(bool value)
        {
            Enum<HingeJoint>(j => j.useMotor = value);
        }

        public void SetUseLimits(bool value)
        {
            Enum<HingeJoint>(j => j.useLimits = value);
        }

        public void SetUseSpring(bool value)
        {
            Enum<HingeJoint>(j => j.useSpring = value);
        }

        //-------------------------------------------------------------------------------
    }
}
