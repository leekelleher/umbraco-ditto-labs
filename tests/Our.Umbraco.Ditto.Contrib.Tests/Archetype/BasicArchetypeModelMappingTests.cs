using Archetype.Models;
using NUnit.Framework;
using Our.Umbraco.Ditto.Mocks;

namespace Our.Umbraco.Ditto.Contrib.Tests.Archetype
{
    [TestFixture]
    public class BasicArchetypeModelMappingTests
    {
        public class BasicModel
        {
            public ArchetypeModel MyProperty { get; set; }
        }

        [Test]
        public void Basic_ArchetypeModel_Mapped()
        {
            var archetype = new ArchetypeModel();

            var property = new MockPublishedProperty("myProperty", archetype, true);
            var content = new MockPublishedContent { Properties = new[] { property } };

            var model = content.As<BasicModel>();

            Assert.IsNotNull(model);
            Assert.IsNotNull(model.MyProperty);
            Assert.IsInstanceOf<ArchetypeModel>(model.MyProperty);
            Assert.IsEmpty(model.MyProperty.Fieldsets);
        }
    }
}