namespace Our.Umbraco.Ditto.Archetype
{
    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using global::Archetype.Models;
    using global::Umbraco.Core;
    using global::Umbraco.Web;

    public static class ArchetypeFieldsetModelExtensions
    {
        private static readonly ConcurrentDictionary<Type, ParameterInfo[]> ConstructorCache
           = new ConcurrentDictionary<Type, ParameterInfo[]>();

        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> PropertyCache
            = new ConcurrentDictionary<Type, PropertyInfo[]>();

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

            // Get the default constructor, parameters and create an instance of the type.
            // Try and return from the cache first. TryGetValue is faster than GetOrAdd.
            ParameterInfo[] constructorParams;
            ConstructorCache.TryGetValue(type, out constructorParams);
            if (constructorParams == null)
            {
                var constructor = type.GetConstructors().OrderBy(x => x.GetParameters().Length).FirstOrDefault();
                if (constructor != null)
                {
                    constructorParams = constructor.GetParameters();
                    ConstructorCache.TryAdd(type, constructorParams);
                }
            }

            // If not already an instance, create an instance of the object
            if (instance == null)
            {
                if (constructorParams != null && constructorParams.Length == 0)
                {
                    // Internally this uses Activator.CreateInstance which is heavily optimized.
                    instance = type.GetInstance();
                }
                else
                {
                    // No valid constructor, but see if the value can be cast to the type
                    if (type.IsInstanceOfType(archetype))
                    {
                        instance = archetype;
                    }
                    else
                    {
                        throw new InvalidOperationException(string.Format("Can't convert ArchetypeFieldsetModel to {0} as the target object does not have a parameterless constructor.", type));
                    }
                }
            }

            // Collect all the properties of the given type and loop through writable ones.
            PropertyInfo[] nonVirtualProperties;
            PropertyCache.TryGetValue(type, out nonVirtualProperties);

            if (nonVirtualProperties == null)
            {
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.CanWrite).ToArray();

                // Split out the properties.
                nonVirtualProperties = properties.ToArray();
                PropertyCache.TryAdd(type, nonVirtualProperties);
            }

            // Now loop through and convert non-virtual properties.
            if (nonVirtualProperties != null && nonVirtualProperties.Any())
            {
                foreach (var propertyInfo in nonVirtualProperties)
                {
                    using (DisposableTimer.DebugDuration<object>(string.Format("ForEach Property ({0})", propertyInfo.Name), "Complete"))
                    {
                        // Check for the ignore attribute.
                        var ignoreAttr = propertyInfo.GetCustomAttribute<DittoIgnoreAttribute>();
                        if (ignoreAttr != null)
                        {
                            continue;
                        }

                        object propertyValue = GetResolvedValue(fieldset, archetype, culture, propertyInfo, instance);
                        object result = GetConvertedValue(fieldset, archetype, culture, propertyInfo, propertyValue, instance);

                        propertyInfo.SetValue(instance, result, null);
                    }
                }
            }

            return instance;
        }

        private static object GetResolvedValue(
            ArchetypeFieldsetModel fieldset,
            ArchetypeModel archetype,
            CultureInfo culture,
            PropertyInfo propertyInfo,
            object instance)
        {
            // Check the property for an associated value attribute, otherwise fall-back on expected behaviour.
            var valueAttr = propertyInfo.GetCustomAttribute<DittoValueResolverAttribute>(true);

            if (valueAttr == null)
            {
                // Default to Archetype fieldset property attribute
                valueAttr = new ArchetypePropertyAttribute();
            }

            // Time custom value-resolver.
            using (DisposableTimer.DebugDuration<object>(string.Format("Custom ValueResolver ({0}, {1})", fieldset.Id, propertyInfo.Name), "Complete"))
            {
                var resolver = (DittoArchetypeValueResolver)valueAttr.ResolverType.GetInstance();

                var context = new DittoArchetypeValueResolverContext
                {
                    Instance = new DittoArchetypeValueResolverModelContainer
                    {
                        Archetype = archetype,
                        Fieldset = fieldset
                    },
                    PropertyDescriptor = TypeDescriptor.GetProperties(instance)[propertyInfo.Name]
                };

                // Resolve value
                return resolver.ResolveValue(context, valueAttr, culture);
            }
        }

        private static object GetConvertedValue(
            ArchetypeFieldsetModel fieldset,
            ArchetypeModel archetype,
            CultureInfo culture,
            PropertyInfo propertyInfo,
            object propertyValue,
            object instance)
        {
            // TODO: [LK] Implement the TypeConverter stuff (copy over from Ditto)
            return propertyValue;
        }
    }
}