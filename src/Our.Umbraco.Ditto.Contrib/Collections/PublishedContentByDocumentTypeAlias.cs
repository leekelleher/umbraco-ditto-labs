using Umbraco.Core;

namespace Our.Umbraco.Ditto
{
    public class PublishedContentByDocumentTypeAlias : PublishedContentFilter
    {
        public PublishedContentByDocumentTypeAlias(string documentTypeAlias = null)
        {
            if (!string.IsNullOrWhiteSpace(documentTypeAlias))
            {
                Filter = x => x.DocumentTypeAlias.InvariantEquals(documentTypeAlias);
            }
        }
    }
}