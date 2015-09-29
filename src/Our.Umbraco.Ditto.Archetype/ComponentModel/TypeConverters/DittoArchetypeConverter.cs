namespace Our.Umbraco.Ditto
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
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

                if (value is ArchetypeModel)
                {
                    return ((ArchetypeModel)value).As(propertyType, culture);
                }

                if (value is ArchetypeFieldsetModel)
                {
                    return ((ArchetypeFieldsetModel)value).As(propertyType, culture);
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}