namespace Our.Umbraco.Ditto
{
	using System;
	using System.ComponentModel;
	using System.Globalization;
	using global::Umbraco.Core.Models;

	public class UmbracoRelationConverter : DittoConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return typeof(IRelation).IsAssignableFrom(sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return typeof(IPublishedContent).IsAssignableFrom(destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (context == null || context.PropertyDescriptor == null || value == null)
			{
				// There's no way to determine the type here.
				return null;
			}

			var propertyType = context.PropertyDescriptor.PropertyType;
			//var isGenericType = propertyType.IsGenericType;
			//var targetType = isGenericType
			//	? propertyType.GenericTypeArguments.First()
			//	: propertyType;

			if (value is IRelation)
			{
				var relation = (IRelation)value;

				return this.ConvertContentFromInt(relation.ChildId, propertyType, culture);
			}

			return base.ConvertFrom(context, culture, value);
		}
	}
}