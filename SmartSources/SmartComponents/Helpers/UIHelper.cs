using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/UI Helper")]
    public class UIHelper : RedirectableHelper
    {
        //-------------------------------------------------------------------------------

        public void SetWorldPosition(Vector3 value)
        {
            Enum<RectTransform>(rt => rt.position = value);
        }

        public void SetWorldPositionX(float value)
        {
            Enum<RectTransform>(rt => rt.position = new Vector3(value, rt.position.y, rt.position.z));
        }

        public void SetWorldPositionY(float value)
        {
            Enum<RectTransform>(rt => rt.position = new Vector3(rt.position.x, value, rt.position.z));
        }

        public void SetWorldPositionZ(float value)
        {
            Enum<RectTransform>(rt => rt.position = new Vector3(rt.position.x, rt.position.y, value));
        }

        //-------------------------------------------------------------------------------

        public void SetLocalPosition(Vector3 value)
        {
            Enum<RectTransform>(rt => rt.localPosition = value);
        }

        public void SetLocalPositionX(float value)
        {
            Enum<RectTransform>(rt => rt.localPosition = new Vector3(value, rt.localPosition.y, rt.localPosition.z));
        }

        public void SetLocalPositionY(float value)
        {
            Enum<RectTransform>(rt => rt.localPosition = new Vector3(rt.localPosition.x, value, rt.localPosition.z));
        }

        public void SetLocalPositionZ(float value)
        {
            Enum<RectTransform>(rt => rt.localPosition = new Vector3(rt.localPosition.x, rt.localPosition.y, value));
        }

        //-------------------------------------------------------------------------------

        public void SetWidth(float value)
        {
            Enum<RectTransform>(rt => rt.sizeDelta = new Vector2(value, rt.sizeDelta.y));
        }

        public void SetHeight(float value)
        {
            Enum<RectTransform>(rt => rt.sizeDelta = new Vector2(rt.sizeDelta.x, value));
        }

       //-------------------------------------------------------------------------------
    }
}