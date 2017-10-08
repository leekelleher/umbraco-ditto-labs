using System.Web;
using Umbraco.Core;

namespace Our.Umbraco.Ditto
{
    public class QueryStringAttribute : DittoProcessorAttribute
    {
        public QueryStringAttribute(string key)
            : this()
        {
            Key = key;
        }

        public QueryStringAttribute()
        { }

        public string Key { get; set; }

        public override object ProcessValue()
        {
            var qs = HttpContext.Current?.Request?.QueryString;
            if (qs == null)
                return null;

            var propertyName = this.Context.PropertyDescriptor != null
                ? Context.PropertyDescriptor.Name
                : string.Empty;

            var key = Key ?? propertyName;

            return qs.AllKeys.InvariantContains(key)
                ? qs.GetValues(key)
                : null;
        }
    }
}