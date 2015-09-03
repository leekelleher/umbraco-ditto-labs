namespace Our.Umbraco.Ditto.Archetype
{
    using global::Archetype.Models;
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ArchetypePropertyAttribute : DittoValueResolverAttribute
    {
        public ArchetypePropertyAttribute()
            : base(typeof(ArchetypePropertyValueResolver))
        {
        }

        public ArchetypePropertyAttribute(
            string propertyName,
            string altPropertyName = "",
            object defaultValue = null)
            : this()
        {
            this.PropertyName = propertyName;
            this.AltPropertyName = altPropertyName;
            this.DefaultValue = defaultValue;
        }

        public string PropertyName { get; set; }

        public string AltPropertyName { get; set; }

        public object DefaultValue { get; set; }
    }
}