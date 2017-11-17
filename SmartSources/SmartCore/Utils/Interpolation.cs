using System;
using UnityEngine;

namespace Smart.Utils
{
    public static class Interpolation
    {
        //--------------------------------------------------------------------------------------------------------------

        public enum Kind
        {
            Linear,
            InSine, OutSine, InOutSine,
            InQuad, OutQuad, InOutQuad,
            InCubic, OutCubic, InOutCubic,
            InQuart, OutQuart, InOutQuart,
            InQuint, OutQuint, InOutQuint,
            InExpo, OutExpo, InOutExpo,
            InCirc, OutCirc, InOutCirc
        }

        public static readonly string[] KindNames = Enum.GetNames(typeof(Kind));

        //--------------------------------------------------------------------------------------------------------------

        private const float Pi = Mathf.PI;
        private const float PiOver2 = Mathf.PI * 0.5f;

        public static float Apply(Kind kind, float t) // 0..1
        {
            switch (kind)
            {
                default: return t;
                case Kind.Linear: return t;
                     
                case Kind.InSine: return 1 - (float)Math.Cos(t * PiOver2);
                case Kind.OutSine: return (float)Math.Sin(t * PiOver2);
                case Kind.InOutSine: return 0.5f * (1 - (float)Math.Cos(Pi * t));
                     
                case Kind.InQuad: return t * t;
                case Kind.OutQuad: return -t * (t - 2);
                case Kind.InOutQuad: return (t *= 2f) < 1 ? 0.5f * t * t : 0.5f * (1 - --t * (t - 2));
                     
                case Kind.InCubic: return t * t * t;
                case Kind.OutCubic: return --t * t * t + 1;
                case Kind.InOutCubic: return (t *= 2f) < 1 ? 0.5f * t * t * t : 0.5f * ((t -= 2) * t * t + 2);
                     
                case Kind.InQuart: return t * t * t * t;
                case Kind.OutQuart: return -(--t * t * t * t - 1);
                case Kind.InOutQuart: return (t *= 2f) < 1 ? 0.5f * t * t * t * t : -0.5f * ((t -= 2) * t * t * t - 2);
                     
                case Kind.InQuint: return t * t * t * t * t;
                case Kind.OutQuint: return --t * t * t * t * t + 1;
                case Kind.InOutQuint: return (t *= 2f) < 1 ? 0.5f * t * t * t * t * t : 0.5f * ((t -= 2) * t * t * t * t + 2);
                     
                case Kind.InExpo: return t == 0 ? 0 : (float)Math.Pow(2, 10 * (t - 1));
                case Kind.OutExpo: return t == 1 ? 1 : -(float)Math.Pow(2, -10 * t) + 1;
                case Kind.InOutExpo: return t == 0 ? 0 : (t == 1 ? 1 : ((t *= 2f) < 1 ? 0.5f * (float)Math.Pow(2, 10 * (t - 1)) : 0.5f * (-(float)Math.Pow(2, -10 * --t) + 2)));
                     
                case Kind.InCirc: return -((float)Math.Sqrt(1 - t * t) - 1);
                case Kind.OutCirc: return (float)Math.Sqrt(1 - --t * t);
                case Kind.InOutCirc: return (t *= 2f) < 1 ? -0.5f * ((float)Math.Sqrt(1 - t * t) - 1) : 0.5f * ((float)Math.Sqrt(1 - (t -= 2) * t) + 1);
            }
        }

        //--------------------------------------------------------------------------------------------------------------

        public static float InSine(float t) { return 1 - (float)Math.Cos(t * PiOver2); }
        public static float OutSine(float t) { return (float)Math.Sin(t * PiOver2); }
        public static float InOutSine(float t) { return 0.5f * (1 - (float)Math.Cos(Pi * t)); }

        public static float InQuad(float t) { return t* t; }
        public static float OutQuad(float t) { return -t* (t - 2); }
        public static float InOutQuad(float t) { return (t *= 2f) < 1 ? 0.5f * t* t : 0.5f * (1 - --t* (t - 2)); }
        
        public static float InCubic(float t) { return t* t * t; }
        public static float OutCubic(float t) { return --t* t * t + 1; }
        public static float InOutCubic(float t) { return (t *= 2f) < 1 ? 0.5f * t* t * t : 0.5f * ((t -= 2) * t* t + 2); }
        
        public static float InQuart(float t) { return t* t * t* t; }
        public static float OutQuart(float t) { return -(--t* t * t* t - 1); }
        public static float InOutQuart(float t) { return (t *= 2f) < 1 ? 0.5f * t* t * t* t : -0.5f * ((t -= 2) * t* t * t - 2); }
        
        public static float InQuint(float t) { return t* t * t* t * t; }
        public static float OutQuint(float t) { return --t* t * t* t * t + 1; }
        public static float InOutQuint(float t) { return (t *= 2f) < 1 ? 0.5f * t* t * t* t * t : 0.5f * ((t -= 2) * t* t * t* t + 2); }
        
        public static float InExpo(float t) { return t == 0 ? 0 : (float)Math.Pow(2, 10 * (t - 1)); }
        public static float OutExpo(float t) { return t == 1 ? 1 : -(float)Math.Pow(2, -10 * t) + 1; }
        public static float InOutExpo(float t) { return t == 0 ? 0 : (t == 1 ? 1 : ((t *= 2f) < 1 ? 0.5f * (float)Math.Pow(2, 10 * (t - 1)) : 0.5f * (-(float)Math.Pow(2, -10 * --t) + 2))); }
        
        public static float InCirc(float t) { return -((float)Math.Sqrt(1 - t* t) - 1); }
        public static float OutCirc(float t) { return (float)Math.Sqrt(1 - --t* t); }
        public static float InOutCirc(float t) { return (t *= 2f) < 1 ? -0.5f * ((float)Math.Sqrt(1 - t* t) - 1) : 0.5f * ((float)Math.Sqrt(1 - (t -= 2) * t) + 1); }

        //--------------------------------------------------------------------------------------------------------------
    }
}
