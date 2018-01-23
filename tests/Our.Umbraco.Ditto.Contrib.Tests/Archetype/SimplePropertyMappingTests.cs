using Archetype.Models;
using Archetype.PropertyConverters;
using NUnit.Framework;
using Our.Umbraco.Ditto.Mocks;

namespace Our.Umbraco.Ditto.Contrib.Tests.Archetype
{
    [TestFixture]
    public class SimplePropertyMappingTests
    {
        private ArchetypeModel _archetype;

        [TestFixtureSetUp]
        public void Init()
        {
            var archetypeJson = "{\"fieldsets\":[{\"properties\":[{\"alias\":\"textField\",\"value\":\"Testing text field\"}],\"alias\":\"myModel\",\"disabled\":false}]}";
            var converter = new ArchetypeValueConverter();

            _archetype = (ArchetypeModel)converter.ConvertDataToSource(null, archetypeJson, false);
        }

        public class MyModel
        {
            [DittoArchetype]
            public SimpleModel MyProperty { get; set; }
        }

        public class SimpleModel
        {
            public string TextField { get; set; }
        }

        [Test]
        public void Simple_Property_Mapped()
        {
            var property = new MockPublishedProperty("myProperty", _archetype, true);
            var content = new MockPublishedContent { Properties = new[] { property } };

            var model = content.As<MyModel>();

            Assert.IsNotNull(model);
            Assert.IsNotNull(model.MyProperty);
            Assert.That(model.MyProperty, Is.InstanceOf<SimpleModel>());
        }
    }
}