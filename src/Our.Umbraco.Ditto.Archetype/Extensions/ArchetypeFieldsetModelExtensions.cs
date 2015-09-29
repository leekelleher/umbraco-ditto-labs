namespace Our.Umbraco.Ditto
{
    using System;
    using System.Globalization;
    using Archetype.Models;
    using global::Umbraco.Core.Models;

    public static class ArchetypeFieldsetModelExtensions
    {
        public static T As<T>(
            this ArchetypeFieldsetModel fieldset,
            CultureInfo culture = null,
            object instance = null)
            where T : class
        {
            return fieldset.As(typeof(T), culture, instance) as T;
        }

        public static object As(
            this ArchetypeFieldsetModel fieldset,
            Type type,
            CultureInfo culture = null,
            object instance = null)
        {
            if (fieldset == null)
            {
                return null;
            }

            return fieldset.ToPublishedContent().As(type, culture, instance);
        }

        public static IPublishedContent ToPublishedContent(this ArchetypeFieldsetModel fieldset)
        {
            return new ArchetypePublishedContent(fieldset);
        }
    }
}