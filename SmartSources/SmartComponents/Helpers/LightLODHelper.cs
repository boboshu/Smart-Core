using Smart.Managers;
using UnityEngine;

namespace Smart.Helpers
{
    [RequireComponent(typeof(Light))]
    [AddComponentMenu("Smart/Helpers/Light LOD Helper")]
    public class LightLODHelper : MonoBehaviour
    {
        public float hardShadowsDistance = 15;
        
        public float noShadowsDistanceBeginLerp = 50;
        public float noShadowsDistance = 60;

        public float cullDistanceBeginLerp = 140;
        public float cullDistance = 150;

        private float _initialIntensity;
        private float _initialShadowsStrength;
        private LightShadows _hardShadowsValue;
        private LightShadows _softShadowsValue;
        private Light _light;

        void Awake()
        {
            _light = GetComponent<Light>();
            UpdateRememberedValues();

            switch (_light.shadows)
            {
                case LightShadows.None:
                    _hardShadowsValue = LightShadows.None;
                    _softShadowsValue = LightShadows.None;
                    break;

                case LightShadows.Hard:
                    _hardShadowsValue = LightShadows.Hard;
                    _softShadowsValue = LightShadows.Hard;
                    break;

                case LightShadows.Soft:
                    _hardShadowsValue = LightShadows.Hard;
                    _softShadowsValue = LightShadows.Soft;
                    break;
            }

            UpdatesManager.Subscribe(OnUpdate, 0.1f); // 10 times per second
        }

        void OnDestroy()
        {
            UpdatesManager.UnSubscribe(OnUpdate);
        }

        private void OnUpdate()
        {
            var cam = Camera.main;
            if (cam == null) return;

            var distance = Vector3.Distance(cam.transform.position, transform.position);

            _light.enabled = distance < cullDistance;

            if (distance > noShadowsDistance) _light.shadows = LightShadows.None;
            else _light.shadows = distance > hardShadowsDistance ? _hardShadowsValue : _softShadowsValue;

            var shadowStrength = Mathf.Clamp01((distance - noShadowsDistanceBeginLerp) / (noShadowsDistance - noShadowsDistanceBeginLerp));
            _light.shadowStrength = Mathf.Lerp(_initialShadowsStrength, 0, shadowStrength);

            var lightIntensity = Mathf.Clamp01((distance - cullDistanceBeginLerp) / (cullDistance - cullDistanceBeginLerp));
            _light.intensity = Mathf.Lerp(_initialIntensity, 0, lightIntensity);
        }

        public void UpdateRememberedValues()
        {
            _initialIntensity = _light.intensity;
            _initialShadowsStrength = _light.shadowStrength;
        }
    }
}
