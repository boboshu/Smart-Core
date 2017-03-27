using Smart.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Smart.Helpers
{
    [RequireComponent(typeof(InputField))]
    [AddComponentMenu("Smart/Helpers/Input Field Helper")]
    public class InputFieldHelper : MonoBehaviour
    {
        public bool allowEmpty;
        public bool onlyIfEnterPressed;
        public UnityEventString onSubmit;

        void Awake()
        {
            var cmp = GetComponent<InputField>();
            cmp.onEndEdit.AddListener(OnEndEdit);
        }

        private void OnEndEdit(string text)
        {
            if (!allowEmpty && string.IsNullOrEmpty(text)) return;
            if (onlyIfEnterPressed && !Input.GetKey(KeyCode.Return)) return;
            onSubmit.Invoke(text);
        }
    }
}