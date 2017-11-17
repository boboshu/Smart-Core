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

        public static Rect Resize(this Rect r, float newSize)
        {
            var center = r.center;
            var halfSize = newSize * 0.5f;

            return new Rect
            {
                xMin = center.x - halfSize,
                xMax = center.x + halfSize,
                yMin = center.y - halfSize,
                yMax = center.y + halfSize
            };
        }

        public static Rect Resize(this Rect r, float newSizeX, float newSizeY)
        {
            var center = r.center;
            var halfSizeX = newSizeX * 0.5f;
            var halfSizeY = newSizeY * 0.5f;

            return new Rect
            {
                xMin = center.x - halfSizeX,
                xMax = center.x + halfSizeX,
                yMin = center.y - halfSizeY,
                yMax = center.y + halfSizeY
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

        public static Rect SubRectBottom(this Rect r, float height)
        {
            return new Rect
            {
                xMin = r.xMin,
                xMax = r.xMax,
                yMin = r.yMax - height,
                yMax = r.yMax
            };
        }

        public static Rect SubRectTop(this Rect r, float height)
        {
            return new Rect
            {
                xMin = r.xMin,
                xMax = r.xMax,
                yMin = r.yMin,
                yMax = r.yMin + height
            };
        }

        public static Rect SubRectLeft(this Rect r, float width)
        {
            return new Rect
            {
                xMin = r.xMin,
                xMax = r.xMin + width,
                yMin = r.yMin,
                yMax = r.yMax
            };
        }

        public static Rect SubRectRight(this Rect r, float width)
        {
            return new Rect
            {
                xMin = r.xMax - width,
                xMax = r.xMax,
                yMin = r.yMin,
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
