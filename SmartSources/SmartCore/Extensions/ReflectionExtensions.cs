using System.Collections.Generic;
using System.Reflection;

namespace Smart.Extensions
{
    public static class ReflectionExtensions
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public static IEnumerable<FieldInfo> Reflection_Fields(this object target)
        {
            var type = target.GetType();
            while (type != typeof(object))
            {
                foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    yield return field;
                }
                type = type.BaseType;
            }
        }

        public static FieldInfo Reflection_Field(this object target, string name)
        {
            var type = target.GetType();
            while (type != typeof(object))
            {
                var fld = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (fld != null) return fld;
                type = type.BaseType;
            }
            return null;
        }

        public static object Reflection_FieldGet(this object target, string name)
        {
            return Reflection_Field(target, name)?.GetValue(target);
        }

        public static void Reflection_FieldSet(this object target, string name, object value)
        {
            Reflection_Field(target, name)?.SetValue(target, value);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static IEnumerable<PropertyInfo> Reflection_Properties(this object target)
        {
            var type = target.GetType();
            while (type != typeof(object))
            {
                foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    yield return prop;
                }
                type = type.BaseType;
            }
        }

        public static PropertyInfo Reflection_Property(this object target, string name)
        {
            var type = target.GetType();
            while (type != typeof(object))
            {
                var prop = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (prop != null) return prop;
                type = type.BaseType;
            }
            return null;
        }

        public static object Reflection_PropertyGet(this object target, string name)
        {
            return Reflection_Property(target, name)?.GetValue(target, null);
        }

        public static void Reflection_PropertySet(this object target, string name, object value)
        {
            Reflection_Property(target, name)?.SetValue(target, value, null);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static MethodInfo Reflection_Method(this object target, string name)
        {
            var type = target.GetType();
            while (type != typeof(object))
            {
                var method = type.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (method != null) return method;
                type = type.BaseType;
            }
            return null;
        }

        public static object Reflection_MethodExec(this object target, string name, params object[] parameters)
        {
            return Reflection_Method(target, name)?.Invoke(target, parameters);
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
