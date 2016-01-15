namespace Our.Umbraco.Ditto.Archetype.Tests
{
    using global::Archetype.Models;
    using NUnit.Framework;

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

            var property = new Shared.Mocks.PublishedPropertyMock("myProperty", archetype, true);
            var content = new Shared.Mocks.PublishedContentMock { Properties = new[] { property } };

            var model = content.As<BasicModel>();

            Assert.IsNotNull(model);
            Assert.IsNotNull(model.MyProperty);
            Assert.IsInstanceOf<ArchetypeModel>(model.MyProperty);
            Assert.IsEmpty(model.MyProperty.Fieldsets);
        }
    }
}