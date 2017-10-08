using Umbraco.Web;

namespace Our.Umbraco.Ditto
{
    public class HasValueAttribute : PublishedContentFilterAttribute
    {
        public HasValueAttribute(string propertyAlias, object value = null)
        {
            if (!string.IsNullOrWhiteSpace(propertyAlias) && value != null)
            {
                Filter = x => value.Equals(x.GetPropertyValue(propertyAlias));
            }
            else if (!string.IsNullOrWhiteSpace(propertyAlias))
            {
                Filter = x => x.HasValue(propertyAlias);
            }
        }
    }
}