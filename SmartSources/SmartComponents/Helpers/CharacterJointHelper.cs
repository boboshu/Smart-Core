using System;
using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Character Joint Helper")]
    public class CharacterJointHelper : JointHelper
    {
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------

        private static SoftJointLimit _softJointLimit;
        private static SoftJointLimitSpring _softJointLimitSpring;
        
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void LowTwistLimit(Action action)
        {
            Enum<CharacterJoint>(j => { _softJointLimit = j.lowTwistLimit; action.Invoke(); j.lowTwistLimit = _softJointLimit; });
        }

        private void HighTwistLimit(Action action)
        {
            Enum<CharacterJoint>(j => { _softJointLimit = j.highTwistLimit; action.Invoke(); j.highTwistLimit = _softJointLimit; });
        }

        private void Swing1Limit(Action action)
        {
            Enum<CharacterJoint>(j => {_softJointLimit = j.swing1Limit; action.Invoke(); j.swing1Limit = _softJointLimit; });
        }

        private void Swing2Limit(Action action)
        {
            Enum<CharacterJoint>(j => { _softJointLimit = j.swing2Limit; action.Invoke(); j.swing2Limit = _softJointLimit; });
        }

        private void TwistLimitSpring(Action action)
        {
            Enum<CharacterJoint>(j => { _softJointLimitSpring = j.twistLimitSpring; action.Invoke(); j.twistLimitSpring = _softJointLimitSpring; });
        }

        private void SwingLimitSpring(Action action)
        {
            Enum<CharacterJoint>(j => { _softJointLimitSpring = j.swingLimitSpring; action.Invoke(); j.swingLimitSpring = _softJointLimitSpring; });
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void SetSwingAxis(Vector3 value)
        {
            Enum<CharacterJoint>(j => j.swingAxis = value);
        }

        public void SetEnableProjection(bool value)
        {
            Enum<CharacterJoint>(j => j.enableProjection = value);
        }

        public void SetProjectionDistance(float value)
        {
            Enum<CharacterJoint>(j => j.projectionDistance = value);
        }

        public void SetProjectionAngle(float value)
        {
            Enum<CharacterJoint>(j => j.projectionAngle = value);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void SetTwistLimitSpring_Spring(float value)
        {
            TwistLimitSpring(() => _softJointLimitSpring.spring = value);
        }

        public void SetTwistLimitSpring_Damper(float value)
        {
            TwistLimitSpring(() => _softJointLimitSpring.damper = value);
        }

        public void SetSwingLimitSpring_Spring(float value)
        {
            SwingLimitSpring(() => _softJointLimitSpring.spring = value);
        }

        public void SetSwingLimitSpring_Damper(float value)
        {
            SwingLimitSpring(() => _softJointLimitSpring.damper = value);
        }

        public void SetLowTwistLimit_Bounciness(float value)
        {
            LowTwistLimit(() => _softJointLimit.bounciness = value);
        }

        public void SetLowTwistLimit_Limit(float value)
        {
            LowTwistLimit(() => _softJointLimit.limit = value);
        }

        public void SetLowTwistLimit_ContactDistance(float value)
        {
            LowTwistLimit(() => _softJointLimit.contactDistance = value);
        }

        public void SetHighTwistLimit_Bounciness(float value)
        {
            HighTwistLimit(() => _softJointLimit.bounciness = value);
        }

        public void SetHighTwistLimit_Limit(float value)
        {
            HighTwistLimit(() => _softJointLimit.limit = value);
        }

        public void SetHighTwistLimit_ContactDistance(float value)
        {
            HighTwistLimit(() => _softJointLimit.contactDistance = value);
        }

        public void SetSwing1Limit_Bounciness(float value)
        {
            Swing1Limit(() => _softJointLimit.bounciness = value);
        }

        public void SetSwing1Limit_Limit(float value)
        {
            Swing1Limit(() => _softJointLimit.limit = value);
        }

        public void SetSwing1Limit_ContactDistance(float value)
        {
            Swing1Limit(() => _softJointLimit.contactDistance = value);
        }

        public void SetSwing2Limit_Bounciness(float value)
        {
            Swing2Limit(() => _softJointLimit.bounciness = value);
        }

        public void SetSwing2Limit_Limit(float value)
        {
            Swing2Limit(() => _softJointLimit.limit = value);
        }

        public void SetSwing2Limit_ContactDistance(float value)
        {
            Swing2Limit(() => _softJointLimit.contactDistance = value);
        }
        
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    }
}
