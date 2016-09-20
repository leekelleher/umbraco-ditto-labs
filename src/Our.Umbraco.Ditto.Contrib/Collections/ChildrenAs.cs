using System.Collections.Generic;

namespace Our.Umbraco.Ditto
{
    public class ChildrenAs : DittoMultiProcessorAttribute
    {
        public ChildrenAs(string documentTypeAlias = null)
        {
            Attributes = new List<DittoProcessorAttribute>()
            {
                new UmbracoPropertyAttribute("Children"),
                new PublishedContentByDocumentTypeAlias(documentTypeAlias)
            };
        }
    }
}