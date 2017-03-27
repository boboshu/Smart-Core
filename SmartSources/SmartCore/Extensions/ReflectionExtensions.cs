using System.Reflection;

namespace Smart.Extensions
{
    public static class ReflectionExtensions
    {
        //--------------------------------------------------------------------------------------------------------------------------

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

        public static object Reflection_FieldValue(this object target, string name)
        {
            return Reflection_Field(target, name)?.GetValue(target);
        }

        //--------------------------------------------------------------------------------------------------------------------------

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

        public static object Reflection_PropertyValue(this object target, string name)
        {
            return Reflection_Property(target, name)?.GetValue(target, null);
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
