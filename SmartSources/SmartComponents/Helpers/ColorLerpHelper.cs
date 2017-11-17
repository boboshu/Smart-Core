using System.Collections;
using Smart.Extensions;
using Smart.Types;
using UnityEngine;
using UnityEngine.Events;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Color Lerp Helper")]
    public class ColorLerpHelper : MonoBehaviour
    {
        [Range(0.1f, 60)] public float duration = 0.5f;

        public Color current = Color.black;
        public Color target = Color.white;
        public UnityEventColor onLerp = new UnityEventColor();

        public UnityEvent onStart = new UnityEvent();
        public UnityEvent onFinish = new UnityEvent();

        //--------------------------------------------------------------------------------------------------------------------------

        public void SetDuration(float value)
        {
            duration = value;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void SetCurrentColor(string value)
        {
            current = value.AsHexColor();
        }

        public void SetTargetColor(string value)
        {
            target = value.AsHexColor();
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void SetCurrentColor(Color value)
        {
            current = value;
        }

        public void SetTargetColor(Color value)
        {
            target = value;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void Lerp()
        {
            if (_counter > 0) StopAllCoroutines();
            if (isActiveAndEnabled) StartCoroutine(DoLerp());
            else current = target;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void LerpFrom(string value)
        {
            current = value.AsHexColor();
            if (_counter > 0) StopAllCoroutines();
            if (isActiveAndEnabled) StartCoroutine(DoLerp());
            else current = target;
        }

        public void LerpTo(string value)
        {
            target = value.AsHexColor();
            if (_counter > 0) StopAllCoroutines();
            if (isActiveAndEnabled) StartCoroutine(DoLerp());
            else current = target;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void LerpFrom(Color value)
        {
            current = value;
            if (_counter > 0) StopAllCoroutines();
            if (isActiveAndEnabled) StartCoroutine(DoLerp());
            else current = target;
        }

        public void LerpTo(Color value)
        {
            target = value;
            if (_counter > 0) StopAllCoroutines();
            if (isActiveAndEnabled) StartCoroutine(DoLerp());
            else current = target;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void Stop()
        {
            StopAllCoroutines();
        }

        private float _counter;

        private IEnumerator DoLerp()
        {
            onStart.Invoke();
            _counter = duration;
            var from = current;

            while (_counter > 0)
            {
                yield return null;
                onLerp.Invoke(current = Color.Lerp(from, target, 1 - _counter / duration));
                _counter -= Time.deltaTime;
            }
            
            onLerp.Invoke(current = target);
            onFinish.Invoke();
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
