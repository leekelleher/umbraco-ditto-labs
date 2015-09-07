namespace Our.Umbraco.Ditto.Archetype.Tests
{
    using System.ComponentModel;
    using global::Archetype.Models;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using Our.Umbraco.Ditto.Contrib;

    [TestFixture]
    public class BasicMappingTests
    {
        public class BasicModel
        {
            public ArchetypeModel MyProperty { get; set; }
        }

        public class BasicModel_TypedProperty
        {
            [TypeConverter(typeof(DittoArchetypeConverter))]
            public SimpleModel MyProperty { get; set; }
        }

        public class SimpleModel
        {
            public string TextField { get; set; }
        }

        [Test]
        public void Basic_Model_Default()
        {
            var archetype = new ArchetypeModel();

            var property = new PublishedPropertyMock("myProperty", archetype, true);
            var content = new PublishedContentMock { Properties = new[] { property } };

            var model = content.As<BasicModel>();

            Assert.IsNotNull(model);
            Assert.IsNotNull(model.MyProperty);
            Assert.IsInstanceOf<ArchetypeModel>(model.MyProperty);
            Assert.IsEmpty(model.MyProperty.Fieldsets);
        }

        [Test]
        public void Basic_Model_Populated()
        {
            var archetypeJson = "{\"fieldsets\":[{\"properties\":[{\"alias\":\"textField\",\"value\":\"Testing text field\"}],\"alias\":\"simpleModel\",\"disabled\":false}]}";
            var archetype = JsonConvert.DeserializeObject<ArchetypeModel>(archetypeJson);

            var property = new PublishedPropertyMock("myProperty", archetype, true);
            var content = new PublishedContentMock { Properties = new[] { property } };

            var model = content.As<BasicModel_TypedProperty>();

            Assert.IsNotNull(model);
            Assert.IsNotNull(model.MyProperty);
            Assert.IsInstanceOf<SimpleModel>(model.MyProperty);
        }
    }
}