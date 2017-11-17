using Smart.Types;
using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Random Helper")]
    public class RandomHelper : MonoBehaviour
    {
        [Range(0.1f, 100)] public float from = 0.1f;
        [Range(0.1f, 100)] public float to = 1f;
        public UnityEventFloat onResult = new UnityEventFloat();

        public void Next()
        {
            if (to > @from) onResult.Invoke(@from + (to - @from) * Random.value);
            else onResult.Invoke(to + (@from - to) * Random.value);
        }
    }
}
