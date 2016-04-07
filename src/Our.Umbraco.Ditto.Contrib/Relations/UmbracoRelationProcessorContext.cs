using System;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Our.Umbraco.Ditto
{
    public class UmbracoRelationProcessorContext : DittoProcessorContext
    {
        public IRelationService RelationService;

        public Func<int, IPublishedContent> GetById;

        public UmbracoRelationProcessorContext()
        {
            if (ApplicationContext.Current != null)
            {
                this.RelationService = ApplicationContext.Current.Services.RelationService;

            }

            if (UmbracoContext.Current != null)
            {
                this.GetById = UmbracoContext.Current.ContentCache.GetById;
            }
        }
    }
}