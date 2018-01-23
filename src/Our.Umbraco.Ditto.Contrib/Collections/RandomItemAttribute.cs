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
            if (Value is IEnumerable<object> items)
            {
                var random = new Random();
                var randomIndex = random.Next(items.Count());

                return items.ElementAt(randomIndex);
            }

            return Value;
        }
    }
}