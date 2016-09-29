using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Our.Umbraco.Ditto.Contrib.Tests.Mocks;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Contrib
{
    [TestFixture]
    public class ChildrenAsTests
    {
        private IPublishedContent Content;

        const string MyDocumentTypeAlias1 = "myDocumentTypeAlias1";
        const string MyDocumentTypeAlias2 = "myDocumentTypeAlias2";
        const string MyDocumentTypeAlias3 = "myDocumentTypeAlias3";

        interface IMyModel
        {
            IEnumerable<IPublishedContent> MyProperty { get; set; }
        }

        public class MyModel1 : IMyModel
        {
            [ChildrenAs(MyDocumentTypeAlias1)]
            public IEnumerable<IPublishedContent> MyProperty { get; set; }
        }

        public class MyModel2 : IMyModel
        {
            [ChildrenAs(MyDocumentTypeAlias1, MyDocumentTypeAlias2)]
            public IEnumerable<IPublishedContent> MyProperty { get; set; }
        }

        public class MyModel3 : IMyModel
        {
            [ChildrenAs]
            public IEnumerable<IPublishedContent> MyProperty { get; set; }
        }

        [TestFixtureSetUp]
        public void Init()
        {
            Content = new MockPublishedContent
            {
                Children = new[]
                {
                    new MockPublishedContent { Id = 1111, Name = "Item 1", DocumentTypeAlias = MyDocumentTypeAlias1 },
                    new MockPublishedContent { Id = 2222, Name = "Item 2", DocumentTypeAlias = MyDocumentTypeAlias2 },
                    new MockPublishedContent { Id = 3333, Name = "Item 3", DocumentTypeAlias = MyDocumentTypeAlias3 },
                }
            };
        }

        [Test]
        [TestCase(typeof(MyModel1), 1)]
        [TestCase(typeof(MyModel2), 2)]
        [TestCase(typeof(MyModel3), 3)]
        public void ChildrenAs_Resolves(Type type, int count, int lastId)
        {
            var model = (IMyModel)Content.As(type);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.MyProperty, Is.Not.Null);

            var list = model.MyProperty.ToList();

            Assert.That(list, Has.Count);
            Assert.That(list.Count, Is.EqualTo(count));
        }
    }
}