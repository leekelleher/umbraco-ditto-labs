namespace Our.Umbraco.Ditto.Archetype
{
    using System.Reflection;
    using global::Umbraco.Core;

    public class ArchetypePropertyValueResolver : DittoArchetypeValueResolver<DittoArchetypeValueResolverContext, ArchetypePropertyAttribute>
    {
        public override object ResolveValue()
        {
            var defaultValue = Attribute.DefaultValue;
            var propName = Context.PropertyDescriptor != null ? Context.PropertyDescriptor.Name : string.Empty;
            var altPropName = string.Empty;

            // Check for Archetype properties attribute on class
            if (Context.PropertyDescriptor != null)
            {
                var classAttr = Context.PropertyDescriptor.ComponentType
                    .GetCustomAttribute<ArchetypePropertiesAttribute>();
                if (classAttr != null)
                {
                    // Apply the prefix
                    if (!string.IsNullOrWhiteSpace(classAttr.Prefix))
                    {
                        altPropName = propName;
                        propName = classAttr.Prefix + propName;
                    }
                }
            }

            var archetypePropertyName = Attribute.PropertyName ?? propName;
            var altArchetypePropertyName = Attribute.AltPropertyName ?? altPropName;

            if (this.Fieldset == null)
            {
                return defaultValue;
            }

            var contentType = this.Fieldset.GetType();
            object propertyValue = null;

            // Try fetching the value.
            if (!archetypePropertyName.IsNullOrWhiteSpace())
            {
                var contentProperty = contentType.GetProperty(archetypePropertyName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static);
                propertyValue = contentProperty != null
                    ? contentProperty.GetValue(this.Fieldset, null)
                    : this.Fieldset.GetValue(archetypePropertyName);
            }

            // Try fetching the alt value.
            if ((propertyValue == null || propertyValue.ToString().IsNullOrWhiteSpace())
                && !string.IsNullOrWhiteSpace(altArchetypePropertyName))
            {
                var contentProperty = contentType.GetProperty(altArchetypePropertyName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static);
                propertyValue = contentProperty != null
                    ? contentProperty.GetValue(this.Fieldset, null)
                    : this.Fieldset.GetValue(altArchetypePropertyName);
            }

            // Try setting the default value.
            if ((propertyValue == null || propertyValue.ToString().IsNullOrWhiteSpace())
                && defaultValue != null)
            {
                propertyValue = defaultValue;
            }

            return propertyValue;
        }
    }
}