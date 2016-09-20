using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Our.Umbraco.Ditto
{
    public abstract class PublishedContentFilter : DittoProcessorAttribute
    {
        protected Func<IPublishedContent, bool> Filter { get; set; }

        public override object ProcessValue()
        {
            var content = Value as IPublishedContent;
            if (content != null)
            {
                return Filter != null && Filter(content)
                    ? content
                    : null;
            }

            var items = Value as IEnumerable<IPublishedContent>;
            if (items != null)
            {
                return Filter != null
                    ? items.Where(Filter)
                    : items;
            }

            return Value;
        }
    }
}