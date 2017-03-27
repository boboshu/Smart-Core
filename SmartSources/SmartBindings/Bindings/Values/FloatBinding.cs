using Smart.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Smart.Bindings.Values
{
    [AddComponentMenu("Smart/Bindings/Values/Float Binding")]
    public class FloatBinding : Binding<float>
    {
        public enum Kind { None, Text, Couner, Call }
        public Kind kind;

        public bool hideZero = false;
        public string format = "0.##";
        public Text text;
        public GameObject[] counterGameObjects = new GameObject[0];
        public UnityEventFloat call;

        protected override void Apply(float value)
        {
            switch (kind)
            {
                case Kind.Text:
                    if (text)
                    {
                        text.enabled = !(string.IsNullOrEmpty(text.text) || (hideZero && Mathf.Abs(value) < 0.01f));
                        try
                        {
                            text.text = string.IsNullOrEmpty(format) ? value.ToString("0.##") : value.ToString(format);
                        }
                        catch  // in case of bad format string
                        {
                            text.text = value.ToString("0.##");
                        }
                    }
                    break;

                case Kind.Couner:
                    var r = Mathf.Round(value);
                    for (var i = 0; i < counterGameObjects.Length; i++)
                        if (counterGameObjects[i]) // is defined
                            counterGameObjects[i].SetActive(i < r);
                    break;

                case Kind.Call:
                    call.Invoke(value);
                    break;
            }
        }

        protected override bool IsEquals(float v1, float v2)
        {
            return Mathf.Abs(v1 - v2) < 0.001f;
        }
    }
}
