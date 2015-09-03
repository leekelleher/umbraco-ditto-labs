namespace Our.Umbraco.Ditto.Archetype
{
    using global::Archetype.Models;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class DittoArchetypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(ArchetypeModel))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is ArchetypeModel && context != null && context.PropertyDescriptor != null)
            {
                var propertyType = context.PropertyDescriptor.PropertyType;
                return ((ArchetypeModel)value).As(propertyType, culture);
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}