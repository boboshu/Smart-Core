using Smart.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Smart.Bindings.Values
{
    [AddComponentMenu("Smart/Bindings/Values/Integer Binding")]
    public class IntegerBinding : Binding<int>
    {
        public enum Kind { None, Text, Couner, Call }
        public Kind kind;

        public bool hideZero = false;
        public string format = "0";
        public Text text;
        public GameObject[] counterGameObjects = new GameObject[0];
        public UnityEventInt call;

        protected override void Apply(int value)
        {
            switch (kind)
            {
                case Kind.Text:
                    if (text)
                    {
                        text.enabled = !(string.IsNullOrEmpty(text.text) || (hideZero && value == 0));
                        try
                        {
                            text.text = string.IsNullOrEmpty(format) ? value.ToString() : value.ToString(format);
                        }
                        catch // in case of bad format string
                        {
                            text.text = value.ToString();
                        }
                    }
                    break;

                case Kind.Couner:
                    var r = value + 1;
                    for (var i = 0; i < counterGameObjects.Length; i++)
                        if (counterGameObjects[i]) // is defined
                            counterGameObjects[i].SetActive(i < r);
                    break;

                case Kind.Call:
                    call.Invoke(value);
                    break;
            }
        }

        protected override bool IsEquals(int v1, int v2)
        {
            return v1 == v2;
        }
    }
}
