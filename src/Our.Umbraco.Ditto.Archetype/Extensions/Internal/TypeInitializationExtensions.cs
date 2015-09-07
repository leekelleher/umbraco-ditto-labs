namespace Our.Umbraco.Ditto
{
    using System;
    using System.Web.Mvc;

    internal static class TypeInitializationExtensions
    {
        public static object GetDependencyResolvedInstance(this Type type)
        {
            return DependencyResolver.Current.GetService(type) ?? GetInstance(type);
        }

        public static object GetInstance(this Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}