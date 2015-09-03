namespace Our.Umbraco.Ditto.Archetype
{
    using global::Archetype.Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

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
            return ConvertArchetypeModel(archetype, type, culture);
        }

        private static object ConvertArchetypeModel(
            ArchetypeModel archetype,
            Type type,
            CultureInfo culture,
            object instance = null)
        {
            if (archetype == null)
            {
                return null;
            }

            var items = new List<object>();

            foreach (var fieldset in archetype.Fieldsets)
            {
                items.Add(fieldset.As(type, culture, archetype, instance));
            }

            return items; // TODO: [LK] Review which object type to return, either List<object> or FirstOrDefault?
        }
    }
}