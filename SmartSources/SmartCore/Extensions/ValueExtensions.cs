using System.Collections.Generic;

namespace Smart.Extensions
{
    public static class ValueExtensions
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public static void Swap<T>(ref T x, ref T y)
        {
            var t = y;
            y = x;
            x = t;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static bool Equals<T>(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
