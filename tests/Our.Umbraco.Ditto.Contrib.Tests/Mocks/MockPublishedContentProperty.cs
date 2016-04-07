using System;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Contrib.Tests.Mocks
{
    public class MockPublishedContentProperty : IPublishedContentProperty
    {
        public MockPublishedContentProperty()
        {
            HasValue = true;
            Alias = "alias";
            Value = null;
        }

        public MockPublishedContentProperty(string alias, object value, bool hasValue)
        {
            HasValue = hasValue;
            Alias = alias;
            Value = value;
        }

        public string PropertyTypeAlias { get; set; }

        public bool HasValue { get; set; }

        public object DataValue { get; set; }

        public object Value { get; set; }

        public object XPathValue { get; set; }

        public string Alias { get; set; }

        public Guid Version { get; set; }
    }
}