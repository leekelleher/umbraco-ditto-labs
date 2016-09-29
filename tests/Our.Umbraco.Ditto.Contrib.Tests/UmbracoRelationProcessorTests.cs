using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Moq;
using NUnit.Framework;
using Our.Umbraco.Ditto.Contrib.Tests.Mocks;
using Umbraco.Core;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.ObjectResolution;
using Umbraco.Core.Profiling;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.PublishedCache;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;

namespace Our.Umbraco.Ditto.Contrib
{
    [TestFixture]
    public class UmbracoRelationProcessorTests
    {
        private IPublishedContent Content;

        [TestFixtureSetUp]
        public void Init()
        {
            var serviceContext = new ServiceContext(null, null, null, null, null, null, null, null, new MockRelationService(), null, null, null, null);
            var cacheHelper = CacheHelper.CreateDisabledCacheHelper();
            var logger = new ProfilingLogger(Mock.Of<ILogger>(), Mock.Of<IProfiler>());

            var appContext = new ApplicationContext(cacheHelper, logger) { Services = serviceContext };
            ApplicationContext.EnsureContext(appContext, true);

            if (!PublishedCachesResolver.HasCurrent)
            {
                var mockPublishedContentCache = new Mock<IPublishedContentCache>();

                mockPublishedContentCache
                    .Setup(x => x.GetById(It.IsAny<UmbracoContext>(), It.IsAny<bool>(), It.IsAny<int>()))
                    .Returns<UmbracoContext, bool, int>((ctx, preview, id) => new MockPublishedContent { Id = id });

                PublishedCachesResolver.Current =
                    new PublishedCachesResolver(new PublishedCaches(mockPublishedContentCache.Object, new Mock<IPublishedMediaCache>().Object));
            }

            UmbracoContext.EnsureContext(
                httpContext: Mock.Of<HttpContextBase>(),
                applicationContext: appContext,
                webSecurity: new Mock<WebSecurity>(null, null).Object,
                umbracoSettings: Mock.Of<IUmbracoSettingsSection>(),
                urlProviders: Enumerable.Empty<IUrlProvider>(),
                replaceContext: true);

            UmbracoPickerHelper.GetMembershipHelper = (ctx) => new MembershipHelper(ctx, Mock.Of<MembershipProvider>(), Mock.Of<RoleProvider>());

            Resolution.Freeze();

            Content = new MockPublishedContent { Id = 8001 };
        }

        public class MyModel<T>
        {
            [UmbracoRelation]
            public IEnumerable<T> RelatedItems { get; set; }
        }

        public class MyRelatedModel
        {
            public int Id { get; set; }
        }

        [Test]
        public void UmbracoRelations_Resolves()
        {
            var model = Content.As<MyModel<IPublishedContent>>();

            Assert.IsNotNull(model.RelatedItems);
            Assert.IsInstanceOf<IEnumerable<IPublishedContent>>(model.RelatedItems);
            Assert.That(model.RelatedItems.Count(), Is.EqualTo(3));
            Assert.That(model.RelatedItems.First().Id, Is.EqualTo(9001));
        }

        [Test]
        public void UmbracoRelations_Resolves_As_Typed()
        {
            var model = Content.As<MyModel<MyRelatedModel>>();

            Assert.IsNotNull(model.RelatedItems);
            Assert.IsInstanceOf<IEnumerable<MyRelatedModel>>(model.RelatedItems);
            Assert.That(model.RelatedItems.Count(), Is.EqualTo(3));
            Assert.That(model.RelatedItems.First().Id, Is.EqualTo(9001));
        }
    }
}