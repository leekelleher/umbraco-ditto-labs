namespace Our.Umbraco.Ditto
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Archetype.Models;
    using global::Umbraco.Core.Models;

    public static class ArchetypeFieldsetModelExtensions
    {
        public static T As<T>(
            this ArchetypeFieldsetModel fieldset,
            CultureInfo culture = null,
            object instance = null,
            IEnumerable<DittoValueResolverContext> valueResolverContexts = null,
            Action<DittoConversionHandlerContext> onConverting = null,
            Action<DittoConversionHandlerContext> onConverted = null)
            where T : class
        {
            return fieldset.As(typeof(T), culture, instance, valueResolverContexts, onConverting, onConverted) as T;
        }

        public static object As(
            this ArchetypeFieldsetModel fieldset,
            Type type,
            CultureInfo culture = null,
            object instance = null,
            IEnumerable<DittoValueResolverContext> valueResolverContexts = null,
            Action<DittoConversionHandlerContext> onConverting = null,
            Action<DittoConversionHandlerContext> onConverted = null)
        {
            if (fieldset == null)
            {
                return null;
            }

            return fieldset.ToPublishedContent().As(type, culture, instance, valueResolverContexts, onConverting, onConverted);
        }

        public static IPublishedContent ToPublishedContent(this ArchetypeFieldsetModel fieldset)
        {
            return new ArchetypePublishedContent(fieldset);
        }
    }
}