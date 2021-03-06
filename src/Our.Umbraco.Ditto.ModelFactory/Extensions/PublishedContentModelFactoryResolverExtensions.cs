﻿using System;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace Our.Umbraco.Ditto
{
    /// <summary>
    /// Encapsulates extension methods for <see cref="PublishedContentModelFactoryResolver"/>.
    /// </summary>
    public static class PublishedContentModelFactoryResolverExtensions
    {
        /// <summary>
        /// Sets the factory resolver to resolve the given types using the <see cref="DittoPublishedContentModelFactory"/>.
        /// </summary>
        /// <param name="resolver">
        /// The <see cref="PublishedContentModelFactoryResolver"/> this method extends.
        /// </param>
        /// <typeparam name="T">
        /// The base <see cref="Type"/> to retrieve classes that inherit from.
        /// </typeparam>
        public static void SetFactory<T>(this PublishedContentModelFactoryResolver resolver)
            where T : IPublishedContent
        {
            var types = PluginManager.Current.ResolveTypes<T>();
            var factory = new DittoPublishedContentModelFactory(types);

            resolver.SetFactory(factory);
        }
    }
}