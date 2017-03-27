using UnityEngine;
using UnityEngine.UI;

namespace Smart.Bindings.Values
{
    [AddComponentMenu("Smart/Bindings/Values/Color Binding")]
    public class ColorBinding : Binding<Color>
    {
        public enum Kind { None, Graphic }
        public Kind kind;

        public Graphic graphic;

        protected override void Apply(Color value)
        {
            switch (kind)
            {
                case Kind.Graphic:
                    if (graphic)
                    {
                        graphic.color = value;
                        graphic.enabled = (value.a < 0.01f);
                    }
                    break;
            }
        }

        protected override bool IsEquals(Color v1, Color v2)
        {
            return Equals(v1, v2);
        }
    }
}
