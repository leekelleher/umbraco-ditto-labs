namespace Our.Umbraco.Ditto.Archetype
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ArchetypePropertiesAttribute: Attribute
    {
        public string Prefix { get; set; }
    }
}