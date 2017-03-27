using UnityEngine;

namespace Smart.Extensions
{
    public static class LightExtensions
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public static Light CreateDirectionalLight(this GameObject root, Vector3 target, Color color, LightShadows shadows = LightShadows.Soft, float intensity = 1)
        {
            var cmp = CreateBaseLight(color, shadows, intensity);
            cmp.Reparent(root);
            cmp.gameObject.transform.LookAt(target);
            cmp.type = LightType.Directional;

            return cmp;
        }

        public static Light CreatePointLight(this GameObject root, Vector3 position, float range, Color color, LightShadows shadows = LightShadows.None, float intensity = 1)
        {
            var cmp = CreateBaseLight(color, shadows, intensity);
            cmp.Reparent(root);
            cmp.gameObject.transform.position = position;
            cmp.type = LightType.Point;
            cmp.range = range;

            return cmp;
        }

        public static Light CreateSpotLight(this GameObject root, Vector3 position, Vector3 target, float range, float angle, Color color, LightShadows shadows = LightShadows.None, float intensity = 1)
        {
            var cmp = CreateBaseLight(color, shadows, intensity);
            cmp.Reparent(root);
            cmp.gameObject.transform.position = position;
            cmp.gameObject.transform.LookAt(target);
            cmp.type = LightType.Point;
            cmp.range = range;
            cmp.spotAngle = angle;

            return cmp;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private static Light CreateBaseLight(Color color, LightShadows shadows, float intensity)
        {
            var go = new GameObject("Light");

            var cmp = go.AddComponent<Light>();
            cmp.color = color;
            cmp.shadows = shadows;
            cmp.intensity = intensity;

            return cmp;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
