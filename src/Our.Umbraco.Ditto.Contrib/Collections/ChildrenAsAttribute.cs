using System.Collections.Generic;

namespace Our.Umbraco.Ditto
{
    public class ChildrenAsAttribute : DittoMultiProcessorAttribute
    {
        public ChildrenAsAttribute(params string[] documentTypeAliases)
        {
            Attributes = new List<DittoProcessorAttribute>()
            {
                new UmbracoPropertyAttribute("Children"),
                new PublishedContentByDocumentTypeAliasAttribute(documentTypeAliases)
            };
        }
    }
}