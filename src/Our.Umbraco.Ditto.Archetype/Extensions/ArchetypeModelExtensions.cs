namespace Our.Umbraco.Ditto
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Archetype.Models;
    using global::Umbraco.Core.Models;

    public static class ArchetypeModelExtensions
    {
        public static T As<T>(
            this ArchetypeModel archetype,
            CultureInfo culture = null,
            IEnumerable<DittoValueResolverContext> valueResolverContexts = null,
            Action<DittoConversionHandlerContext> onConverting = null,
            Action<DittoConversionHandlerContext> onConverted = null)
            where T : class
        {
            return archetype.As(typeof(T), culture, valueResolverContexts, onConverting, onConverted) as T;
        }

        public static object As(
            this ArchetypeModel archetype,
            Type type,
            CultureInfo culture = null,
            IEnumerable<DittoValueResolverContext> valueResolverContexts = null,
            Action<DittoConversionHandlerContext> onConverting = null,
            Action<DittoConversionHandlerContext> onConverted = null)
        {
            if (archetype == null || archetype.Fieldsets == null)
            {
                return null;
            }

            return archetype.ToPublishedContentSet().As(type, culture, valueResolverContexts, onConverting, onConverted);
        }

        public static IEnumerable<IPublishedContent> ToPublishedContentSet(this ArchetypeModel archetype)
        {
            return new ArchetypePublishedContentSet(archetype);
        }
    }
}