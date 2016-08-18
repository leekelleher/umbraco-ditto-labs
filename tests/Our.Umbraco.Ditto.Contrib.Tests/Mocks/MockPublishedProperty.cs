using System;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Contrib.Tests.Mocks
{
    public class MockPublishedProperty : IPublishedProperty
    {
        public MockPublishedProperty()
        {
            HasValue = true;
            PropertyTypeAlias = "alias";
            Value = null;
        }

        public MockPublishedProperty(string alias, object value, bool hasValue)
        {
            HasValue = hasValue;
            PropertyTypeAlias = alias;
            Value = value;
        }

        public string PropertyTypeAlias { get; set; }

        public bool HasValue { get; set; }

        public object DataValue { get; set; }

        public object Value { get; set; }

        public object XPathValue { get; set; }

        public Guid Version { get; set; }
    }
}