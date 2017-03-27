using System;

namespace Smart.Types
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DescriptionAttribute : Attribute
    {
        private readonly string _text;

        public string Text => _text;

        public DescriptionAttribute(string text)
        {
            _text = text;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class FormatAttribute : Attribute
    {
        private readonly string _text;

        public string Text => _text;

        public FormatAttribute(string text)
        {
            _text = text;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class FirstInListAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class)]
    public class ClearTransformAttribute : Attribute { }
}
