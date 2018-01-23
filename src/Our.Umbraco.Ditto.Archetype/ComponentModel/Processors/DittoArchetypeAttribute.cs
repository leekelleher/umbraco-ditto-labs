using Archetype.Extensions;
using Archetype.Models;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto
{
    public class DittoArchetypeAttribute : UmbracoPropertyAttribute
    {
        public DittoArchetypeAttribute()
            : base()
        { }

        public DittoArchetypeAttribute(
            string propertyName,
            string altPropertyName = null,
            bool recursive = false,
            object defaultValue = null)
            : base(propertyName, altPropertyName, recursive, defaultValue)
        { }

        public override object ProcessValue()
        {
            var value = this.Value;

            if (value is IPublishedContent)
            {
                value = base.ProcessValue();
            }

            if (value is ArchetypeModel)
            {
                return ((ArchetypeModel)value).ToPublishedContentSet();
            }

            if (value is ArchetypeFieldsetModel)
            {
                return ((ArchetypeFieldsetModel)value).ToPublishedContent();
            }

            return value;
        }
    }
}