using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Collections
{
    public enum OrderBy
    {
        ContentTree = 1,
        Name = 2,
        SortOrder = 3,
    }

    [DittoProcessorMetaData(ValueType = typeof(IEnumerable<IPublishedContent>))]
    public class OrderByAttribute : DittoProcessorAttribute
    {
        public OrderBy OrderBy { get; set; }

        public OrderByAttribute(OrderBy orderBy = OrderBy.SortOrder)
        {
            OrderBy = orderBy;
        }

        public override object ProcessValue()
        {
            var items = Value as IEnumerable<IPublishedContent>;

            switch (this.OrderBy)
            {
                case OrderBy.ContentTree:
                    return items
                        .OrderBy(x => x.Parent != null ? x.Parent.SortOrder : 1)
                        .ThenBy(x => x.SortOrder);

                case OrderBy.Name:
                    return items
                        .OrderBy(x => x.Name);

                case OrderBy.SortOrder:
                    return items
                        .OrderBy(x => x.SortOrder);

                default:
                    return items;
            }
        }
    }
}