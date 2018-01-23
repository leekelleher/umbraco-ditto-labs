using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Our.Umbraco.Ditto
{
    public abstract class PublishedContentFilterAttribute : DittoProcessorAttribute
    {
        protected Func<IPublishedContent, bool> Filter { get; set; }

        public override object ProcessValue()
        {
            if (Value is IPublishedContent content)
            {
                return Filter != null && Filter(content)
                    ? content
                    : null;
            }

            if (Value is IEnumerable<IPublishedContent> items)
            {
                return Filter != null
                    ? items.Where(Filter)
                    : items;
            }

            return Value;
        }
    }
}