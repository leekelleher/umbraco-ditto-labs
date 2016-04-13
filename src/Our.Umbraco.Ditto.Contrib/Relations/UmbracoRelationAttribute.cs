using System.Linq;

namespace Our.Umbraco.Ditto
{
    public class UmbracoRelationAttribute : DittoMultiProcessorAttribute
    {
        public UmbracoRelationAttribute()
            : this(string.Empty, RelationDirection.ChildToParent)
        { }

        public UmbracoRelationAttribute(string relationTypeAlias, RelationDirection relationDirection = RelationDirection.ChildToParent)
            : base(Enumerable.Empty<DittoProcessorAttribute>())
        {
            this.Attributes.Add(new UmbracoRelationProcessorAttribute(relationTypeAlias, relationDirection));
            this.Attributes.Add(new UmbracoPickerAttribute());
        }
    }
}