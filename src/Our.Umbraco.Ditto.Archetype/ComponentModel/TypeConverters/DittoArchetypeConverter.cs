namespace Our.Umbraco.Ditto
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using Archetype.Extensions;
    using Archetype.Models;

    public class DittoArchetypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(ArchetypeModel) || sourceType == typeof(ArchetypeFieldsetModel))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (context != null && context.PropertyDescriptor != null)
            {
                var propertyType = context.PropertyDescriptor.PropertyType;
                var isGenericType = propertyType.IsGenericType;
                var targetType = isGenericType
                    ? propertyType.GenericTypeArguments.First()
                    : propertyType;

                if (value is ArchetypeModel)
                {
                    return ((ArchetypeModel)value).ToPublishedContentSet().As(targetType, culture);
                }

                if (value is ArchetypeFieldsetModel)
                {
                    return ((ArchetypeFieldsetModel)value).ToPublishedContent().As(targetType, culture);
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}