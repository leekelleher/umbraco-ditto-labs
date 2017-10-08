using System;
using System.Collections.Generic;
using System.Linq;

namespace Our.Umbraco.Ditto.Collections
{
    [DittoProcessorMetaData(ValueType = typeof(IEnumerable<object>))]
    public class RandomItemAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var items = Value as IEnumerable<object>;

            if (items != null)
            {
                var random = new Random();
                var randomIndex = random.Next(items.Count());

                return items.ElementAt(randomIndex);
            }

            return Value;
        }
    }
}