using System;
using System.Linq;
using Umbraco.Core;

namespace Our.Umbraco.Ditto
{
    public class DelimitedListAttribute : DittoProcessorAttribute
    {
        protected string _delimiter;

        public DelimitedListAttribute(string delimiter = ",")
        {
            _delimiter = delimiter;
        }

        public override object ProcessValue()
        {
            var strValue = Value == null
                ? string.Empty
                : Value.ToString();

            return string.IsNullOrWhiteSpace(strValue)
                ? Enumerable.Empty<string>()
                : strValue.Split(new[] { _delimiter }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .Where(x => x.IsNullOrWhiteSpace() == false)
                    .ToList();
        }
    }
}