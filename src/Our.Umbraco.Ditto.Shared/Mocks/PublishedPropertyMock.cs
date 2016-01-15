namespace Our.Umbraco.Ditto.Shared.Mocks
{
    using System;
    using global::Umbraco.Core.Models;

    public class PublishedPropertyMock : IPublishedProperty
    {
        public PublishedPropertyMock()
             : this("alias", null, true)
        {
        }

        public PublishedPropertyMock(string alias, object value, bool hasValue)
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
    }
}