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
        const string MyDocumentTypeAlias = "myDocumentTypeAlias";

        public class MyModel
        {
            [ChildrenAs(MyDocumentTypeAlias)]
            public IEnumerable<IPublishedContent> MyProperty { get; set; }
        }

        [Test]
        public void ChildrenAs_Resolves()
        {
            var content = new MockPublishedContent
            {
                Children = new[]
                {
                    new MockPublishedContent { Id = 1111, Name = "Item 1", DocumentTypeAlias = MyDocumentTypeAlias },
                    new MockPublishedContent { Id = 2222, Name = "Item 2", DocumentTypeAlias = "myDifferentDocumentTypeAlias" },
                    new MockPublishedContent { Id = 3333, Name = "Item 3", DocumentTypeAlias = MyDocumentTypeAlias },
                }
            };

            var model = content.As<MyModel>();

            Assert.That(model, Is.Not.Null);
            Assert.That(model.MyProperty, Is.Not.Null);

            var list = model.MyProperty.ToList();

            Assert.That(list, Has.Count);
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list.First().Id, Is.EqualTo(1111));
            Assert.That(list.Last().Id, Is.EqualTo(3333));
        }
    }
}