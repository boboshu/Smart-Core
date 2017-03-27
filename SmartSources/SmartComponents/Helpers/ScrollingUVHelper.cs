using UnityEngine;

namespace Smart.Helpers
{
    [RequireComponent(typeof(Renderer))]
    public class ScrollingUVHelper : MonoBehaviour
    {
        public int materialIndex = 0;
        public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
        public string textureName = "_MainTex";

        private Vector2 _uvOffset = Vector2.zero;

        void LateUpdate()
        {
            _uvOffset += (uvAnimationRate * Time.deltaTime);
            if (GetComponent<Renderer>().enabled)
                GetComponent<Renderer>().materials[materialIndex].SetTextureOffset(textureName, _uvOffset);
        }
    }
}
