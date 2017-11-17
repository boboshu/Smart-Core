using UnityEngine;

namespace Smart.Helpers
{
    public abstract class JointHelper : RedirectableHelper
    {
        //-------------------------------------------------------------------------------

        public void SetConnectedBody(Rigidbody value)
        {
            Enum<Joint>(j => j.connectedBody = value);
        }

        public void SetConnectedAxis(Vector3 value)
        {
            Enum<Joint>(j => j.axis = value);
        }

        public void SetAnchor(Vector3 value)
        {
            Enum<Joint>(j => j.anchor = value);
        }

        public void SetConnectedAnchor(Vector3 value)
        {
            Enum<Joint>(j => j.connectedAnchor = value);
        }

        public void SetAutoConfigureConnectedAnchor(bool value)
        {
            Enum<Joint>(j => j.autoConfigureConnectedAnchor = value);
        }

        public void SetEnableCollision(bool value)
        {
            Enum<Joint>(j => j.enableCollision = value);
        }

        public void SetEnablePreprocessing(bool value)
        {
            Enum<Joint>(j => j.enablePreprocessing = value);
        }

        public void SetBreakForce(float value)
        {
            Enum<Joint>(j => j.breakForce = value);
        }

        public void SetBreakTorque(float value)
        {
            Enum<Joint>(j => j.breakTorque = value);
        }
        
        //-------------------------------------------------------------------------------

    }
}
