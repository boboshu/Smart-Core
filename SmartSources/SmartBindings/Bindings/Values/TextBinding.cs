using System.Linq;
using Smart.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Smart.Bindings.Values
{
    [AddComponentMenu("Smart/Bindings/Values/Text Binding")]
    public class TextBinding : Binding<string>
    {
        public enum Kind { None, Text, ResourceSprite, Call }
        public Kind kind;

        public string format = "{s}";
        public string folder = "Sprites";
        public Text text;
        public Image image;
        public UnityEventString call;
        public UnityEventGameObject callGameObject;

        protected override void Apply(string value)
        {
            switch (kind)
            {
                case Kind.Text:
                    if (text)
                    {
                        var v = value ?? "";
                        text.text = (format == "{s}") ? v : format.Replace("{s}", v);
                        text.enabled = (!string.IsNullOrEmpty(text.text));
                    }
                    break;

                case Kind.ResourceSprite:
                    if (image)
                    {
                        image.sprite = string.IsNullOrEmpty(value) ? null : Resources.LoadAll<Sprite>(folder ?? "").FirstOrDefault(x => x.name == value);
                        image.enabled = (image.sprite != null);
                    }
                    break;

                case Kind.Call:
                    call.Invoke(value);
                    break;
            }
        }

        protected override bool IsEquals(string v1, string v2)
        {
            return string.Equals(v1, v2);
        }
    }
}
