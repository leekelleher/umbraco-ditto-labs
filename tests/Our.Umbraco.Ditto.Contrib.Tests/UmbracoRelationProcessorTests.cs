using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Our.Umbraco.Ditto.Contrib.Tests.Mocks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

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
            var appContext = new ApplicationContext(new DatabaseContext(null), serviceContext, new CacheHelper());
            ApplicationContext.EnsureContext(appContext, true);

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