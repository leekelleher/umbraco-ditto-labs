using Umbraco.Web;

namespace Our.Umbraco.Ditto
{
    public class CurrentPageAsAttribute : CurrentContentAsAttribute
    {
        public override object ProcessValue()
        {
            if (UmbracoContext == null || UmbracoContext.PageId.HasValue == false)
                return null;

            return UmbracoContext.ContentCache.GetById(UmbracoContext.PageId.Value);
        }
    }
}