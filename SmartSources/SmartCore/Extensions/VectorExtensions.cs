using UnityEngine;

namespace Smart.Extensions
{
    public static class VectorExtensions
    {
        //--------------------------------------------------------------------------------------------------------------------------

        /// <summary> Clamp rotation from 0..360 to -180..180 </summary>
        public static Vector3 ClampRotation360To180(this Vector3 v)
        {
            if (v.x > 180) v.x -= 360;
            if (v.y > 180) v.y -= 360;
            if (v.z > 180) v.z -= 360;
            return v;
        }

        public static Vector3 ClampVelocity(this Vector3 v, float max)
        {
            return v.sqrMagnitude > max * max ? v.normalized * max : v;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static Vector3 ChangeX(this Vector3 v, float newX)
        {
            return new Vector3(newX, v.y, v.z);
        }

        public static Vector3 ChangeY(this Vector3 v, float newY)
        {
            return new Vector3(v.x, newY, v.z);
        }

        public static Vector3 ChangeZ(this Vector3 v, float newZ)
        {
            return new Vector3(v.x, v.y, newZ);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static bool IsInRect(this Vector2 v, Rect rect)
        {
            return (v.x >= rect.xMin) && (v.x <= rect.xMax) && (v.y >= rect.yMin) && (v.y <= rect.yMax);
        }

        public static Vector3 Multiply(this Vector3 v, Vector3 d)
        {
            return new Vector3(v.x * d.x, v.y * d.y, v.z * d.z);
        }

        public static Vector3 Divide(this Vector3 v, Vector3 d)
        {
            return new Vector3(v.x / d.x, v.y / d.y, v.z / d.z);
        }

        public static Vector3 Divide(this Vector3 v, float f)
        {
            return new Vector3(v.x / f, v.y / f, v.z / f);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static Vector3 GetXYZ(this Vector4 v)
        {
            return new Vector3(v.x, v.y, v.z);
        }

        public static Vector2 GetXY(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static Vector4 V4(Vector3 v)
        {
            return new Vector4(v.x, v.y, v.z);
        }

        public static Vector4 V4(Vector3 v, float w)
        {
            return new Vector4(v.x, v.y, v.z, w);
        }

        public static Vector4 V4(float f)
        {
            return new Vector4(f, f, f, f);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static Vector3 V3(Vector2 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }

        public static Vector3 V3(float f)
        {
            return new Vector3(f, f, f);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static Vector2 V2(float f)
        {
            return new Vector2(f, f);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static bool IsValid(this Vector3 v)
        {
            return !(float.IsNaN(v.x) || float.IsInfinity(v.x) || float.IsNaN(v.y) || float.IsInfinity(v.y) || float.IsNaN(v.z) || float.IsInfinity(v.z));
        }

        public static bool IsValid(this Vector2 v)
        {
            return !(float.IsNaN(v.x) || float.IsInfinity(v.x) || float.IsNaN(v.y) || float.IsInfinity(v.y));
        }
        public static bool IsValid(this Vector4 v)
        {
            return !(float.IsNaN(v.x) || float.IsInfinity(v.x) || float.IsNaN(v.y) || float.IsInfinity(v.y) || float.IsNaN(v.z) || float.IsInfinity(v.z) || float.IsNaN(v.w) || float.IsInfinity(v.w));
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
