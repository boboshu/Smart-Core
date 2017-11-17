using UnityEngine;

namespace Smart.Helpers
{
    public class ScrollingUVHelper : RedirectableHelper
    {
        public int materialIndex = 0;
        public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
        public string textureName = "_MainTex";

        private Vector2 _uvOffset = Vector2.zero;


        void LateUpdate()
        {
            _uvOffset += uvAnimationRate * Time.deltaTime;
            Enum<Renderer>(r => { if (r.enabled) r.materials[materialIndex].SetTextureOffset(textureName, _uvOffset); });                        
        }

        public void SetAntimateRateX(float value)
        {
            uvAnimationRate.x = value;
        }

        public void SetAntimateRateY(float value)
        {
            uvAnimationRate.y = value;
        }
    }
}
