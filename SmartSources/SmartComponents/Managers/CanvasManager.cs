using System.Collections.Generic;
using System.Linq;
using Smart.Extensions;
using Smart.Types;
using UnityEngine;
using UnityEngine.Events;

namespace Smart.Managers
{
    [ClearTransform]
    [AddComponentMenu("Smart/Managers/Canvas Manager")]
    public class CanvasManager : MonoBehaviour
    {
        public bool dontDestroyOnLoad = true;
        public Canvas initialCanvas;

        public UnityEvent onOpen;
        public UnityEvent onOpenPopup;
        public UnityEvent onClosePopup;

        public Canvas activeCanvas;
        private readonly Dictionary<Canvas, Canvas> _popups = new Dictionary<Canvas, Canvas>();
        public static CanvasManager instance;

        void Awake()
        {
            instance = this;
            if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
            Open(initialCanvas);
        }

        void OnDestroy()
        {
            instance = null;
        }

        //-----------------------------------------------------------------------------------------------------------------------

        public void Open(Canvas prefab)
        {
            if (instance == null) return;

            if (instance.activeCanvas != null)
            {
                DestroyImmediate(instance.activeCanvas.gameObject);
                instance.activeCanvas = null;
            }

            if (prefab != null)
            {
                instance.activeCanvas = Instantiate(prefab);
                instance.activeCanvas.Reparent(this);
                instance.activeCanvas.name = instance.activeCanvas.name.Replace("(Clone)", "");
                onOpen.Invoke();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------

        public Canvas OpenPopup(Canvas prefab)
        {
            if (prefab == null) return null;
            Canvas canvas;
            if (_popups.TryGetValue(prefab, out canvas)) return canvas;
            _popups.Add(prefab, canvas = Instantiate(prefab));
            canvas.Reparent(this);
            canvas.name = canvas.name.Replace("(Clone)", "");
            onOpenPopup.Invoke();
            return canvas;
        }

        public void ClosePopup(Canvas prefab)
        {
            if (prefab == null) return;

            // when closing itself prefab value == instance
            foreach (var pair in _popups.Where(x => x.Key == prefab || x.Value == prefab).ToArray())
            {
                _popups.Remove(pair.Key);
                DestroyImmediate(pair.Value.gameObject);
                onClosePopup.Invoke();
            }
        }

        public void CloseAllPopups()
        {
            foreach (var prefab in _popups.Keys.ToArray())
                ClosePopup(prefab);
        }
    }
}