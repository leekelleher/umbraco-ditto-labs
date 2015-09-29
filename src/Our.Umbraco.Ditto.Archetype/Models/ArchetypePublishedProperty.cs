namespace Our.Umbraco.Ditto.Archetype
{
    using global::Archetype.Models;
    using global::Umbraco.Core.Models;

    public class ArchetypePublishedProperty : IPublishedProperty
    {
        private ArchetypePropertyModel _property;

        public ArchetypePublishedProperty(ArchetypePropertyModel property)
        {
            _property = property;
        }

        public object DataValue
        {
            get { return this.Value; }
        }

        public bool HasValue
        {
            get
            {
                if (_property == null || _property.Value == null)
                {
                    return false;
                }

                return !string.IsNullOrEmpty(_property.Value.ToString());
            }
        }

        public string PropertyTypeAlias
        {
            get
            {
                if (_property != null)
                {
                    return _property.PropertyEditorAlias;
                }

                return null;
            }
        }

        public object Value
        {
            get
            {
                if (this.HasValue)
                {
                    return _property.Value;
                }

                return null;
            }
        }

        public object XPathValue
        {
            get { return this.Value; }
        }
    }
}