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
            CultureInfo culture = null)
            where T : class
        {
            return archetype.As(typeof(T), culture) as T;
        }

        public static object As(
            this ArchetypeModel archetype,
            Type type,
            CultureInfo culture = null)
        {
            if (archetype == null || archetype.Fieldsets == null)
            {
                return null;
            }

            return archetype.ToPublishedContentSet().As(type, culture);
        }

        public static IEnumerable<IPublishedContent> ToPublishedContentSet(this ArchetypeModel archetype)
        {
            return new ArchetypePublishedContentSet(archetype);
        }
    }
}