using System;
using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Configurable Joint Helper")]
    public class ConfigurableJointHelper : JointHelper
    {

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------

        private static SoftJointLimit _softJointLimit;
        private static JointDrive _jointDrive;
        private static SoftJointLimitSpring _softJointLimitSpring;

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void LinearLimit(Action action)
        {
            Enum<ConfigurableJoint>(j => { _softJointLimit = j.linearLimit; action.Invoke(); j.linearLimit = _softJointLimit; });
        }

        private void LowAngularXLimit(Action action)
        {
            Enum<ConfigurableJoint>(j => { _softJointLimit = j.lowAngularXLimit; action.Invoke(); j.lowAngularXLimit = _softJointLimit; });
        }

        private void HighAngularXLimit(Action action)
        {
            Enum<ConfigurableJoint>(j => { _softJointLimit = j.highAngularXLimit; action.Invoke(); j.highAngularXLimit = _softJointLimit; });
        }

        private void AngularYLimit(Action action)
        {
            Enum<ConfigurableJoint>(j => { _softJointLimit = j.angularYLimit; action.Invoke(); j.angularYLimit = _softJointLimit; });
        }

        private void AngularZLimit(Action action)
        {
            Enum<ConfigurableJoint>(j => { _softJointLimit = j.angularZLimit; action.Invoke(); j.angularZLimit = _softJointLimit; });
        }

        private void XDrive(Action action)
        {
            Enum<ConfigurableJoint>(j => { _jointDrive = j.xDrive; action.Invoke(); j.xDrive = _jointDrive; });
        }

        private void YDrive(Action action)
        {
            Enum<ConfigurableJoint>(j => { _jointDrive = j.yDrive; action.Invoke(); j.yDrive = _jointDrive; });
        }

        private void ZDrive(Action action)
        {
            Enum<ConfigurableJoint>(j => { _jointDrive = j.zDrive; action.Invoke(); j.zDrive = _jointDrive; });
        }

        private void AngularXDrive(Action action)
        {
            Enum<ConfigurableJoint>(j => { _jointDrive = j.angularXDrive; action.Invoke(); j.angularXDrive = _jointDrive; });
        }

        private void AngularYZDrive(Action action)
        {
            Enum<ConfigurableJoint>(j => { _jointDrive = j.angularYZDrive; action.Invoke(); j.angularYZDrive = _jointDrive; });
        }

        private void SlerpDrive(Action action)
        {
            Enum<ConfigurableJoint>(j => { _jointDrive = j.slerpDrive; action.Invoke(); j.slerpDrive = _jointDrive; });
        }

        private void AngularXLimitSpring(Action action)
        {
            Enum<ConfigurableJoint>(j => { _softJointLimitSpring = j.angularXLimitSpring; action.Invoke(); j.angularXLimitSpring = _softJointLimitSpring; });
        }

        private void AngularYZLimitSpring(Action action)
        {
            Enum<ConfigurableJoint>(j => { _softJointLimitSpring = j.angularYZLimitSpring; action.Invoke(); j.angularYZLimitSpring = _softJointLimitSpring; });
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void SetSecondaryAxis(Vector3 value)
        {
            Enum<ConfigurableJoint>(j => j.secondaryAxis = value);
        }

        public void SetXMotion(int value)
        {
            Enum<ConfigurableJoint>(j => j.xMotion = (ConfigurableJointMotion)value);
        }

        public void SetYMotion(int value)
        {
            Enum<ConfigurableJoint>(j => j.yMotion = (ConfigurableJointMotion)value);
        }
        public void SetZMotion(int value)
        {
            Enum<ConfigurableJoint>(j => j.zMotion = (ConfigurableJointMotion)value);
        }

        public void SetAngularXMotion(int value)
        {
            Enum<ConfigurableJoint>(j => j.angularXMotion = (ConfigurableJointMotion)value);
        }

        public void SetAngularYMotion(int value)
        {
            Enum<ConfigurableJoint>(j => j.angularYMotion = (ConfigurableJointMotion)value);
        }

        public void SetAngularZMotion(int value)
        {
            Enum<ConfigurableJoint>(j => j.angularZMotion = (ConfigurableJointMotion)value);
        }

        public void SetLinearLimitSpring_Spring(float value)
        {
            Enum<ConfigurableJoint>(j => { var x = j.linearLimitSpring; x.spring = value; j.linearLimitSpring = x; });
        }

        public void SetLinearLimitSpring_Damper(float value)
        {
            Enum<ConfigurableJoint>(j => { var x = j.linearLimitSpring; x.damper = value; j.linearLimitSpring = x; });
        }

        public void SetAngularXLimitSpring_Spring(float value)
        {
            AngularXLimitSpring(() => _softJointLimitSpring.spring = value);
        }

        public void SetAngularXLimitSpring_Damper(float value)
        {
            AngularXLimitSpring(() => _softJointLimitSpring.damper = value);
        }

        public void SetAngularYZLimitSpring_Spring(float value)
        {
            AngularYZLimitSpring(() => _softJointLimitSpring.spring = value);
        }

        public void SetAngularYZLimitSpring_Damper(float value)
        {
            AngularYZLimitSpring(() => _softJointLimitSpring.damper = value);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void SetLinearLimit_Limit(float value)
        {
            LinearLimit(() => _softJointLimit.limit = value);
        }

        public void SetLinearLimit_Bounciness(float value)
        {
            LinearLimit(() => _softJointLimit.bounciness = value);
        }

        public void SetLinearLimit_ContactDistance(float value)
        {
            LinearLimit(() => _softJointLimit.contactDistance = value);
        }

        public void SetLowAngularXLimit_Limit(float value)
        {
            LowAngularXLimit(() => _softJointLimit.limit = value);
        }

        public void SetLowAngularXLimit_Bounciness(float value)
        {
            LowAngularXLimit(() => _softJointLimit.bounciness = value);
        }

        public void SetLowAngularXLimit_ContactDistance(float value)
        {
            LowAngularXLimit(() => _softJointLimit.contactDistance = value);
        }

        public void SetHighAngularXLimit_Limit(float value)
        {
            HighAngularXLimit(() => _softJointLimit.limit = value);
        }

        public void SetHighAngularXLimit_Bounciness(float value)
        {
            HighAngularXLimit(() => _softJointLimit.bounciness = value);
        }

        public void SetHighAngularXLimit_ContactDistance(float value)
        {
            HighAngularXLimit(() => _softJointLimit.contactDistance = value);
        }

        public void SetAngularYLimit_Limit(float value)
        {
            AngularYLimit(() => _softJointLimit.limit = value);
        }

        public void SetAngularYLimit_Bounciness(float value)
        {
            AngularYLimit(() => _softJointLimit.bounciness = value);
        }

        public void SetAngularYLimit_ContactDistance(float value)
        {
            AngularYLimit(() => _softJointLimit.contactDistance = value);
        }

        public void SetAngularZLimit_Limit(float value)
        {
            AngularZLimit(() => _softJointLimit.limit = value);
        }

        public void SetAngularZLimit_Bounciness(float value)
        {
            AngularZLimit(() => _softJointLimit.bounciness = value);
        }

        public void SetAngularZLimit_ContactDistance(float value)
        {
            AngularZLimit(() => _softJointLimit.contactDistance = value);
        }

        public void SetTargetPosition(Vector3 value)
        {
            Enum<ConfigurableJoint>(j => j.targetPosition = value);
        }

        public void SetTargetVelocity(Vector3 value)
        {
            Enum<ConfigurableJoint>(j => j.targetVelocity = value);
        }

        public void SetTargetRotation(Quaternion value)
        {
            Enum<ConfigurableJoint>(j => j.targetRotation = value);
        }

        public void SetTargetAngularVelocity(Vector3 value)
        {
            Enum<ConfigurableJoint>(j => j.targetAngularVelocity = value);
        }

        public void SetRotationDriveMode(int value)
        {
            Enum<ConfigurableJoint>(j => j.rotationDriveMode = (RotationDriveMode)value);
        }

        public void SetProjectionMode(int value)
        {
            Enum<ConfigurableJoint>(j => j.projectionMode = (JointProjectionMode)value);
        }

        public void SetProjectionDistance(float value)
        {
            Enum<ConfigurableJoint>(j => j.projectionDistance = value);
        }

        public void SetProjectionAngle(float value)
        {
            Enum<ConfigurableJoint>(j => j.projectionAngle = value);
        }

        public void SetConfiguredInWorldSpace(bool value)
        {
            Enum<ConfigurableJoint>(j => j.configuredInWorldSpace = value);
        }

        public void SetSwapBodies(bool value)
        {
            Enum<ConfigurableJoint>(j => j.swapBodies = value);
        }

        public void SetXDrive_PositionSpring(float value)
        {
            XDrive(() => _jointDrive.positionSpring = value);
        }

        public void SetXDrive_PositionDamper(float value)
        {
            XDrive(() => _jointDrive.positionDamper = value);
        }

        public void SetXDrive_MaximumForce(float value)
        {
            XDrive(() => _jointDrive.maximumForce = value);
        }

        public void SetYDrive_PositionSpring(float value)
        {
            YDrive(() => _jointDrive.positionSpring = value);
        }

        public void SetYDrive_PositionDamper(float value)
        {
            YDrive(() => _jointDrive.positionDamper = value);
        }

        public void SetYDrive_MaximumForce(float value)
        {
            YDrive(() => _jointDrive.maximumForce = value);
        }

        public void SetZDrive_PositionSpring(float value)
        {
            ZDrive(() => _jointDrive.positionSpring = value);
        }

        public void SetZDrive_PositionDamper(float value)
        {
            ZDrive(() => _jointDrive.positionDamper = value);
        }

        public void SetZDrive_MaximumForce(float value)
        {
            ZDrive(() => _jointDrive.maximumForce = value);
        }
        
        public void SetAngularXDrive_PositionSpring(float value)
        {
            AngularXDrive(() => _jointDrive.positionSpring = value);
        }

        public void SetAngularXDrive_PositionDamper(float value)
        {
            AngularXDrive(() => _jointDrive.positionDamper = value);
        }

        public void SetAngularXDrive_MaximumForce(float value)
        {
            AngularXDrive(() => _jointDrive.maximumForce = value);
        }

        public void SetAngularYZDrive_PositionSpring(float value)
        {
            AngularYZDrive(() => _jointDrive.positionSpring = value);
        }

        public void SetAngularYZDrive_PositionDamper(float value)
        {
            AngularYZDrive(() => _jointDrive.positionDamper = value);
        }

        public void SetAngularYZDrive_MaximumForce(float value)
        {
            AngularYZDrive(() => _jointDrive.maximumForce = value);
        }

        public void SetSlerpDrive_PositionSpring(float value)
        {
            SlerpDrive(() => _jointDrive.positionSpring = value);
        }

        public void SetSlerpDrive_PositionDamper(float value)
        {
            SlerpDrive(() => _jointDrive.positionDamper = value);
        }

        public void SetSlerpDrive_MaximumForce(float value)
        {
            SlerpDrive(() => _jointDrive.maximumForce = value);
        }
        
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
