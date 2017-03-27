using Smart.Managers;
using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Popup Helper")]
    public class PopupHelper : MonoBehaviour
    {
        public Canvas popupCanvas;

        void Awake()
        {
            if (popupCanvas && CanvasManager.instance)
                CanvasManager.instance.OpenPopup(popupCanvas);
        }

        void OnDestroy()
        {
            if (popupCanvas && CanvasManager.instance)
                CanvasManager.instance.ClosePopup(popupCanvas);
        }
    }
}
