using System.Linq;
using Umbraco.Core;

namespace Our.Umbraco.Ditto
{
    public class PublishedContentByDocumentTypeAliasAttribute : PublishedContentFilterAttribute
    {
        public PublishedContentByDocumentTypeAliasAttribute(params string[] documentTypeAliases)
        {
            if (documentTypeAliases != null && documentTypeAliases.Any())
            {
                Filter = x => documentTypeAliases.InvariantContains(x.DocumentTypeAlias);
            }
        }
    }
}