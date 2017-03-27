using System.Collections;
using Smart.Types;
using UnityEngine;
using UnityEngine.Events;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Float Lerp Helper")]
    public class FloatLerpHelper : MonoBehaviour
    {
        [Range(0.1f, 60)] public float duration = 0.5f;

        public float current = 0;
        public float target = 1;
        public UnityEventFloat onLerp = new UnityEventFloat();

        public UnityEvent onStart = new UnityEvent();
        public UnityEvent onFinish = new UnityEvent();

        public void SetCurrentFloat(float value)
        {
            current = value;
        }

        public void SetTargetFloat(float value)
        {
            target = value;
        }

        public void Lerp()
        {
            if (_counter > 0) StopAllCoroutines();
            StartCoroutine(DoLerp());
        }

        public void LerpFrom(float value)
        {
            current = value;
            if (_counter > 0) StopAllCoroutines();
            StartCoroutine(DoLerp());
        }

        public void LerpTo(float value)
        {
            target = value;
            if (_counter > 0) StopAllCoroutines();
            StartCoroutine(DoLerp());
        }

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
                onLerp.Invoke(current = Mathf.Lerp(from, target, 1 - _counter / duration));
                _counter -= Time.deltaTime;
            }

            onLerp.Invoke(current = target);
            onFinish.Invoke();
        }
    }
}
