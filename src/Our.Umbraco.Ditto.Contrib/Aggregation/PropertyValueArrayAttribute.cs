using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Aggregation
{
    public class PropertyValueArrayAttribute : UmbracoPropertyAttribute
    {
        protected IEnumerable<string> PropertyAliases { get; set; }

        public PropertyValueArrayAttribute(params string[] propertyAliases)
        {
            PropertyAliases = propertyAliases;
        }

        public override object ProcessValue()
        {
            var targetType = this.Context.PropertyDescriptor.PropertyType;

            if (targetType == this.Value.GetType())
                return this.Value;

            var content = GetPublishedContent(this.Value);

            var innerType = targetType.IsGenericType
                    ? targetType.GenericTypeArguments.First()
                    : targetType;

            var listType = typeof(List<>).MakeGenericType(innerType);

            var items = (IList)Activator.CreateInstance(listType);

            foreach (var alias in PropertyAliases)
            {
                base.PropertyName = alias;
                var propValue = base.ProcessValue();
                var attempt = propValue.TryConvertTo(innerType);

                if (attempt.Success && attempt.Result != null)
                {
                    items.Add(attempt.Result);
                }
            }

            return items;
        }

        private IPublishedContent GetPublishedContent(object value)
        {
            if (value is IPublishedContent)
                return (IPublishedContent)value;

            if (value is IEnumerable<IPublishedContent>)
                return ((IEnumerable<IPublishedContent>)value).FirstOrDefault();

            return this.Context.Content;
        }
    }
}