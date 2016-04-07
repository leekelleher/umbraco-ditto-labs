using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Our.Umbraco.Ditto.Contrib.Tests.Mocks;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Contrib
{
    [TestFixture]
    public class UmbracoRelationProcessorTests
    {
        private IPublishedContent Content;
        private UmbracoRelationProcessorContext RelationContext;

        [TestFixtureSetUp]
        public void Init()
        {
            Content = new MockPublishedContent { Id = 8001 };

            RelationContext = new UmbracoRelationProcessorContext
            {
                RelationService = new MockRelationService(),
                GetById = (x) => new MockPublishedContent { Id = x }
            };
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
            var model = Content.As<MyModel<IPublishedContent>>(processorContexts: new[] { RelationContext });

            Assert.IsNotNull(model.RelatedItems);
            Assert.IsInstanceOf<IEnumerable<IPublishedContent>>(model.RelatedItems);
            Assert.That(model.RelatedItems.Count(), Is.EqualTo(3));
        }

        [Test]
        public void UmbracoRelations_Resolves_As_Typed()
        {
            var model = Content.As<MyModel<MyRelatedModel>>(processorContexts: new[] { RelationContext });

            Assert.IsNotNull(model.RelatedItems);
            Assert.IsInstanceOf<IEnumerable<MyRelatedModel>>(model.RelatedItems);
            Assert.That(model.RelatedItems.Count(), Is.EqualTo(3));
            Assert.That(model.RelatedItems.First().Id, Is.EqualTo(9001));
        }
    }
}