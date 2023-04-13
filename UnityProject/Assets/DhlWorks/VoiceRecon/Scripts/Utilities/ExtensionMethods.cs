using System;
using System.Reflection;

namespace dhlworks.utilities
{
    public static class ExtensionMethods
    {
        public static string GetStringValue(this Enum Value)
        {
            Type t = Value.GetType();

            FieldInfo fi = t.GetField(Value.ToString());

            StringValueAttribute[] attributes = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            return attributes.Length > 0 ? attributes[0].StringValue : null;
        }
    }
}