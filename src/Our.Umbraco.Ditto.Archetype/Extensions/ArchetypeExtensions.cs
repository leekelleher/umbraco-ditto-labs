namespace Our.Umbraco.Ditto
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Archetype.Extensions;
    using Archetype.Models;

    public static class ArchetypeExtensions
    {
        public static IEnumerable<T> As<T>(
            this ArchetypeModel archetype,
            CultureInfo culture = null,
            IEnumerable<DittoValueResolverContext> valueResolverContexts = null,
            Action<DittoConversionHandlerContext> onConverting = null,
            Action<DittoConversionHandlerContext> onConverted = null)
            where T : class
        {
            return archetype.As(typeof(T), culture, valueResolverContexts, onConverting, onConverted) as IEnumerable<T>;
        }

        public static IEnumerable<object> As(
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
    }
}