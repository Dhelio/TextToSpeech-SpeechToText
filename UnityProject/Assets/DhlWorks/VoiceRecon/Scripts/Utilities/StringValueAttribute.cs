using System;

namespace dhlworks.utilities
{
    public class StringValueAttribute : Attribute
    {
        public string StringValue { get; protected set; }

        public StringValueAttribute(string Value) { this.StringValue = Value; }
    }
}