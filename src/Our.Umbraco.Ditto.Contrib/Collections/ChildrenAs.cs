using System.Collections.Generic;

namespace Our.Umbraco.Ditto
{
    public class ChildrenAs : DittoMultiProcessorAttribute
    {
        public ChildrenAs(params string[] documentTypeAliases)
        {
            Attributes = new List<DittoProcessorAttribute>()
            {
                new UmbracoPropertyAttribute("Children"),
                new PublishedContentByDocumentTypeAlias(documentTypeAliases)
            };
        }
    }
}