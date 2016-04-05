namespace Our.Umbraco.Ditto
{
    using System.Linq;
    using Archetype.Models;
    using global::Umbraco.Core.Models;

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

            if (Context != null && Context.PropertyDescriptor != null)
            {
                var propertyType = Context.PropertyDescriptor.PropertyType;
                var isGenericType = propertyType.IsGenericType;
                var targetType = isGenericType
                    ? propertyType.GenericTypeArguments.First()
                    : propertyType;

                if (value is ArchetypeModel)
                {
                    return ((ArchetypeModel)value).As(targetType, Context.Culture);
                }

                if (value is ArchetypeFieldsetModel)
                {
                    return ((ArchetypeFieldsetModel)value).As(targetType, Context.Culture);
                }
            }

            return value;
        }
    }
}