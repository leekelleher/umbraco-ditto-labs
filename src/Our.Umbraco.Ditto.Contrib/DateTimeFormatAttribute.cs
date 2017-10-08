using System;

namespace Our.Umbraco.Ditto
{
    public class DateTimeFormatAttribute : DittoProcessorAttribute
    {
        public string Format { get; private set; }

        public DateTimeFormatAttribute(string format)
        {
            Format = format;
        }

        public override object ProcessValue()
        {
            if (Value is DateTime)
                return ((DateTime)Value).ToString(Format);

            if (Value is string && DateTime.TryParse(Value as string, out DateTime dateTime))
                return dateTime.ToString(Format);

            return Value;
        }
    }
}