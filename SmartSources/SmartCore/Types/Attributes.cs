using System;

namespace Smart.Types
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DescriptionAttribute : Attribute
    {
        public string Text { get; }

        public DescriptionAttribute(string text)
        {
            Text = text;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class FormatAttribute : Attribute
    {
        public string Text { get; }

        public FormatAttribute(string text)
        {
            Text = text;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class FirstInListAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class)]
    public class ClearTransformAttribute : Attribute { }
}
