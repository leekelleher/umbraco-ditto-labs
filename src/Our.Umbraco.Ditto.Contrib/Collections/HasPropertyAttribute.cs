using Umbraco.Web;

namespace Our.Umbraco.Ditto
{
    public class HasPropertyAttribute : PublishedContentFilterAttribute
    {
        public HasPropertyAttribute(string propertyAlias)
        {
            if (!string.IsNullOrWhiteSpace(propertyAlias))
            {
                Filter = x => x.HasProperty(propertyAlias);
            }
        }
    }
}