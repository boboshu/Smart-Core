using UnityEngine;

namespace Smart.Extensions
{
    public static class RectExtensions
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public static Rect Offset(this Rect r, float offsetX, float offsetY)
        {
            return new Rect
            {
                xMin = r.xMin + offsetX,
                xMax = r.xMax + offsetX,
                yMin = r.yMin + offsetY,
                yMax = r.yMax + offsetY
            };
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static Rect Collapse(this Rect r, float offset)
        {
            return new Rect
            {
                xMin = r.xMin + offset,
                xMax = r.xMax - offset,
                yMin = r.yMin + offset,
                yMax = r.yMax - offset
            };
        }

        public static Rect Collapse(this Rect r, float offsetX, float offsetY)
        {
            return new Rect
            {
                xMin = r.xMin + offsetX,
                xMax = r.xMax - offsetX,
                yMin = r.yMin + offsetY,
                yMax = r.yMax - offsetY
            };
        }

        public static Rect Collapse(this Rect r, float offsetLeft, float offsetRight, float offsetTop, float offsetBottom)
        {
            return new Rect
            {
                xMin = r.xMin + offsetLeft,
                xMax = r.xMax - offsetRight,
                yMin = r.yMin + offsetTop,
                yMax = r.yMax - offsetBottom
            };
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static Rect Expand(this Rect r, float offset)
        {
            return new Rect
            {
                xMin = r.xMin - offset,
                xMax = r.xMax + offset,
                yMin = r.yMin - offset,
                yMax = r.yMax + offset
            };
        }

        public static Rect Expand(this Rect r, float offsetX, float offsetY)
        {
            return new Rect
            {
                xMin = r.xMin - offsetX,
                xMax = r.xMax + offsetX,
                yMin = r.yMin - offsetY,
                yMax = r.yMax + offsetY
            };
        }

        public static Rect Expand(this Rect r, float offsetLeft, float offsetRight, float offsetTop, float offsetBottom)
        {
            return new Rect
            {
                xMin = r.xMin - offsetLeft,
                xMax = r.xMax + offsetRight,
                yMin = r.yMin - offsetTop,
                yMax = r.yMax + offsetBottom
            };
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static Rect SubRectBottom(this Rect r, float offset)
        {
            return new Rect
            {
                xMin = r.xMin,
                xMax = r.xMax,
                yMin = r.yMax - offset,
                yMax = r.yMax
            };
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static Rect HalfBottomLeft(this Rect r)
        {
            return new Rect
            {
                xMin = r.xMin,
                xMax = (r.xMin + r.xMax) * 0.5f,
                yMin = (r.yMin + r.yMax) * 0.5f,
                yMax = r.yMax
            };
        }

        public static Rect HalfTopRight(this Rect r)
        {
            return new Rect
            {
                xMin = (r.xMin + r.xMax) * 0.5f,
                xMax = r.xMax,
                yMin = r.yMin,
                yMax = (r.yMin + r.yMax) * 0.5f
            };
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
