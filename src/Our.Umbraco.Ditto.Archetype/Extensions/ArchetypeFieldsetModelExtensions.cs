namespace Our.Umbraco.Ditto.Archetype
{
    using System;
    using System.Globalization;
    using global::Archetype.Models;
    using global::Umbraco.Core;
    using global::Umbraco.Web;

    public static class ArchetypeFieldsetModelExtensions
    {
        public static T As<T>(
            this ArchetypeFieldsetModel fieldset,
            CultureInfo culture = null,
            ArchetypeModel archetype = null,
            object instance = null)
               where T : class
        {
            return fieldset.As(typeof(T), culture, archetype, instance) as T;
        }

        public static object As(
            this ArchetypeFieldsetModel fieldset,
            Type type,
            CultureInfo culture = null,
            ArchetypeModel archetype = null,
            object instance = null)
        {
            using (DisposableTimer.DebugDuration<object>(string.Format("ArchetypeFieldsetModel As ({0})", type.Name), "Complete"))
            {
                return ConvertArchetypeFieldsetModel(fieldset, archetype, type, culture, instance);
            }
        }

        public static ArchetypePublishedContent ToPublishedContent(this ArchetypeFieldsetModel fieldset)
        {
            return new ArchetypePublishedContent(fieldset);
        }

        private static object ConvertArchetypeFieldsetModel(
            ArchetypeFieldsetModel fieldset,
            ArchetypeModel archetype,
            Type type,
            CultureInfo culture,
            object instance = null)
        {
            if (fieldset == null)
            {
                return null;
            }

            // Check if the culture has been set, otherwise use from Umbraco, or fallback to a default
            if (culture == null)
            {
                culture = UmbracoContext.Current != null && UmbracoContext.Current.PublishedContentRequest != null
                    ? UmbracoContext.Current.PublishedContentRequest.Culture
                    : culture = CultureInfo.CurrentCulture;
            }

            return fieldset.ToPublishedContent().As(type, culture, instance);
        }
    }
}