using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Dont Destroy Helper")]
    public class DontDestroyHelper : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
