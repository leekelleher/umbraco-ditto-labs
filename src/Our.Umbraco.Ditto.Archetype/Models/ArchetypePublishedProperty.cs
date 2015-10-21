﻿namespace Our.Umbraco.Ditto
{
    using System;
    using System.Reflection;
    using Archetype.Models;
    using global::Umbraco.Core.Models;
    using global::Umbraco.Core.Models.PublishedContent;
    using global::Umbraco.Core.PropertyEditors;
    using global::Umbraco.Web;

    public class ArchetypePublishedProperty : IPublishedProperty
    {
        private readonly object _rawValue;
        private readonly Lazy<object> _sourceValue;
        private readonly Lazy<object> _objectValue;
        private readonly Lazy<object> _xpathValue;
        private readonly ArchetypePropertyModel _property;
        private readonly PublishedPropertyType _propertyType;
        private readonly PublishedContentType _hostContentType;

        public ArchetypePublishedProperty(ArchetypePropertyModel property, PublishedContentType hostContentType)
        {
            var preview = false;

            _property = property;
            _rawValue = property.Value;

            if (hostContentType == null)
            {
                hostContentType = _property
                    .GetType()
                    .GetProperty("HostContentType", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    .GetValue(_property) as PublishedContentType;
            }

            _hostContentType = hostContentType;

            _propertyType = this.CreateDummyPropertyType();

            if (_propertyType != null)
            {
                _sourceValue = new Lazy<object>(() => _propertyType.ConvertDataToSource(_rawValue, preview));
                _objectValue = new Lazy<object>(() => _propertyType.ConvertSourceToObject(_sourceValue.Value, preview));
                _xpathValue = new Lazy<object>(() => _propertyType.ConvertSourceToXPath(_sourceValue.Value, preview));
            }
        }

        internal ArchetypePropertyModel ArchetypeProperty
        {
            get { return _property; }
        }

        public object DataValue
        {
            get
            {
                return _sourceValue != null
                    ? _sourceValue.Value
                    : _rawValue;
            }
        }

        public bool HasValue
        {
            get
            {
                if (_property == null || _rawValue == null)
                {
                    return false;
                }

                return !string.IsNullOrEmpty(_rawValue.ToString());
            }
        }

        public string PropertyTypeAlias
        {
            get
            {
                if (_propertyType != null && !string.IsNullOrWhiteSpace(_propertyType.PropertyTypeAlias))
                {
                    return _propertyType.PropertyTypeAlias;
                }
                else if (_property != null)
                {
                    return _property.Alias;
                }

                return null;
            }
        }

        public object Value
        {
            get
            {
                return _objectValue != null
                    ? _objectValue.Value
                    : _rawValue;
            }
        }

        public object XPathValue
        {
            get
            {
                return _xpathValue != null
                    ? _xpathValue.Value
                    : _rawValue;
            }
        }

        private PublishedPropertyType CreateDummyPropertyType()
        {
            if (!PropertyValueConvertersResolver.HasCurrent)
            {
                return null;
            }

            var dtd = new DataTypeDefinition(-1, _property.PropertyEditorAlias) { Id = _property.DataTypeId };
            var propertyType = new PropertyType(dtd);

            return new PublishedPropertyType(_hostContentType, propertyType);
        }
    }
}