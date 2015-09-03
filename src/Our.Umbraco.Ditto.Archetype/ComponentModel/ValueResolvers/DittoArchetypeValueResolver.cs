namespace Our.Umbraco.Ditto.Archetype
{
    using global::Archetype.Models;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public abstract class DittoArchetypeValueResolver
        : DittoValueResolver<DittoValueResolverContext, DittoValueResolverAttribute>
    {
        public new DittoArchetypeValueResolverContext Context { get; set; }

        public ArchetypeModel Archetype { get; protected set; }

        public ArchetypeFieldsetModel Fieldset { get; protected set; }

        internal virtual object ResolveValue(
            DittoArchetypeValueResolverContext context,
            DittoValueResolverAttribute attribute,
            CultureInfo culture)
        {
            var container = context.Instance as DittoArchetypeValueResolverModelContainer;

            this.Fieldset = container.Fieldset;
            this.Archetype = container.Archetype;

            this.Context = context;
            this.Attribute = attribute;
            this.Culture = culture;

            return this.ResolveValue();
        }
    }

    public abstract class DittoArchetypeValueResolver<TContextType, TAttributeType>
        : DittoArchetypeValueResolver
        where TContextType : DittoArchetypeValueResolverContext
        where TAttributeType : DittoValueResolverAttribute
    {
        public new TAttributeType Attribute { get; protected set; }

        internal override object ResolveValue(
            DittoArchetypeValueResolverContext context,
            DittoValueResolverAttribute attribute,
            CultureInfo culture)
        {
            if (!(attribute is TAttributeType))
            {
                throw new ArgumentException(
                    "The resolver attribute must be of type " + typeof(TAttributeType).AssemblyQualifiedName,
                    "attribute");
            }

            var container = context.Instance as DittoArchetypeValueResolverModelContainer;

            this.Fieldset = container.Fieldset;
            this.Archetype = container.Archetype;

            this.Context = context;
            this.Attribute = attribute as TAttributeType;
            this.Culture = culture;

            return this.ResolveValue();
        }
    }

    internal class DittoArchetypeValueResolverModelContainer
    {
        public ArchetypeModel Archetype { get; set; }
        public ArchetypeFieldsetModel Fieldset { get; set; }
    }

    public class DittoArchetypeValueResolverContext
        : DittoValueResolverContext
    {
        public new object Instance { get; set; }
        public new PropertyDescriptor PropertyDescriptor { get; set; }
    }
}