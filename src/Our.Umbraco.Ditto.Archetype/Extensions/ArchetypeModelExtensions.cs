namespace Our.Umbraco.Ditto.Archetype
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using global::Archetype.Models;

    public static class ArchetypeModelExtensions
    {
        public static T As<T>(
            this ArchetypeModel archetype,
            CultureInfo culture = null,
            object instance = null)
             where T : class
        {
            return archetype.As(typeof(T), culture, instance) as T;
        }

        public static object As(
            this ArchetypeModel archetype,
            Type type,
            CultureInfo culture = null,
            object instance = null)
        {
            return ConvertArchetypeModel(archetype, type, culture, instance);
        }

        private static object ConvertArchetypeModel(
            ArchetypeModel archetype,
            Type type,
            CultureInfo culture,
            object instance = null)
        {
            if (archetype == null || archetype.Fieldsets == null)
            {
                return null;
            }

            var items = new List<object>();

            foreach (var fieldset in archetype.Fieldsets)
            {
                items.Add(fieldset.As(type, culture, archetype, instance));
            }

            return items;
        }
    }
}