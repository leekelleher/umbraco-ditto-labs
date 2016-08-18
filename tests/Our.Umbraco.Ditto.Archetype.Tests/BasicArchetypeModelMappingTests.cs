using Archetype.Models;
using NUnit.Framework;

namespace Our.Umbraco.Ditto.Archetype.Tests
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

            var property = new Shared.Mocks.MockPublishedProperty("myProperty", archetype, true);
            var content = new Shared.Mocks.MockPublishedContent { Properties = new[] { property } };

            var model = content.As<BasicModel>();

            Assert.IsNotNull(model);
            Assert.IsNotNull(model.MyProperty);
            Assert.IsInstanceOf<ArchetypeModel>(model.MyProperty);
            Assert.IsEmpty(model.MyProperty.Fieldsets);
        }
    }
}