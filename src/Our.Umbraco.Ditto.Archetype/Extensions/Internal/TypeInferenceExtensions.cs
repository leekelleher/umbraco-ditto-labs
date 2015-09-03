namespace Our.Umbraco.Ditto.Archetype
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class TypeInferenceExtensions
    {
        public static bool IsEnumerableType(this Type type)
        {
            return type.TryGetElementType(typeof(IEnumerable<>)) != null;
        }

        public static Type TryGetElementType(this Type type, Type interfaceOrBaseType)
        {
            if (!type.IsGenericTypeDefinition)
            {
                Type[] types = GetGenericTypeImplementations(type, interfaceOrBaseType).ToArray();
                return types.Length == 1 ? types[0].GetGenericArguments().FirstOrDefault() : null;
            }

            return null;
        }

        public static IEnumerable<Type> GetGenericTypeImplementations(this Type type, Type interfaceOrBaseType)
        {
            if (!type.IsGenericTypeDefinition)
            {
                return (interfaceOrBaseType.IsInterface ? type.GetInterfaces() : type.GetBaseTypes())
                        .Union(new[] { type })
                        .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == interfaceOrBaseType);
            }

            return Enumerable.Empty<Type>();
        }

        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            type = type.BaseType;

            while (type != null)
            {
                yield return type;

                type = type.BaseType;
            }
        }
    }
}