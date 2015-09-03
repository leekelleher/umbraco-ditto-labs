using System;

namespace Our.Umbraco.Ditto.Archetype
{
    public class ArchetypePropertyValueResolver : DittoArchetypeValueResolver<DittoArchetypeValueResolverContext, ArchetypePropertyAttribute>
    {
        public override object ResolveValue()
        {
            var propertyAlias = this.Context.PropertyDescriptor != null ? this.Context.PropertyDescriptor.Name : string.Empty;
            var archetypePropertyAlias = this.Attribute.PropertyName ?? propertyAlias;

            return this.Fieldset.GetValue(archetypePropertyAlias);
        }
    }
}