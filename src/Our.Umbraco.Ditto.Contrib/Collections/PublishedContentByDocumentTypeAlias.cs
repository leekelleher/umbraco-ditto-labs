using System.Linq;
using Umbraco.Core;

namespace Our.Umbraco.Ditto
{
    internal class PublishedContentByDocumentTypeAlias : PublishedContentFilter
    {
        public PublishedContentByDocumentTypeAlias(params string[] documentTypeAliases)
        {
            if (documentTypeAliases != null && documentTypeAliases.Any())
            {
                Filter = x => documentTypeAliases.InvariantContains(x.DocumentTypeAlias);
            }
        }
    }
}