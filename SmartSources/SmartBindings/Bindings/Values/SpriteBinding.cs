using UnityEngine;
using UnityEngine.UI;

namespace Smart.Bindings.Values
{
    [AddComponentMenu("Smart/Bindings/Values/Sprite Binding")]
    public class SpriteBinding : Binding<Sprite>
    {
        public enum Kind { None, Image }
        public Kind kind;

        public Image image;

        protected override void Apply(Sprite value)
        {
            switch (kind)
            {
                case Kind.Image:
                    if (image)
                    {
                        image.sprite = value;
                        image.enabled = (value != null);
                    }
                    break;
            }
        }

        protected override bool IsEquals(Sprite v1, Sprite v2)
        {
            return v1 == v2;
        }
    }
}
