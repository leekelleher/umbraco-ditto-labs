using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Our.Umbraco.Ditto
{
    public class AggregateValuesAttribute : DittoProcessorAttribute
    {
        public AggregateValuesAttribute()
            : this(null)
        { }

        public AggregateValuesAttribute(string propertyName)
            : this(propertyName, false)
        { }

        public AggregateValuesAttribute(string propertyName, bool reverseOrder)
        {
            this.PropertyName = propertyName;
            this.ReverseOrder = reverseOrder;
        }

        public string PropertyName { get; set; }

        public bool ReverseOrder { get; set; }

        public override object ProcessValue()
        {
            var targetType = this.Context.PropertyDescriptor.PropertyType;

            if (targetType == this.Value.GetType())
                return this.Value;

            var content = this.Value is IPublishedContent
                ? this.Value as IPublishedContent
                : this.Context.Content;

            var propName = this.Context.PropertyDescriptor != null ? this.Context.PropertyDescriptor.Name : string.Empty;
            var umbracoPropertyName = this.PropertyName ?? propName;

            var innerType = targetType.IsGenericType
                ? targetType.GenericTypeArguments.First()
                : targetType;

            var listType = typeof(List<>).MakeGenericType(innerType);

            var items = (IList)Activator.CreateInstance(listType);

            while (content != null)
            {
                if (content.HasValue(umbracoPropertyName))
                {
                    // TODO: [LK:2016-08-13] Ask MB if it's possible to reuse `UmbracoPropertyAttribute` here?

                    var propValue = content.GetPropertyValue(umbracoPropertyName);
                    var attempt = propValue.TryConvertTo(innerType);

                    if (attempt.Success && attempt.Result != null)
                    {
                        if (ReverseOrder)
                        {
                            items.Insert(0, attempt.Result);
                        }
                        else
                        {
                            items.Add(attempt.Result);
                        }
                    }
                }

                content = content.Parent;
            }

            return items;
        }
    }
}