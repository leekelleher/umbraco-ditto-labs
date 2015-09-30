namespace Our.Umbraco.Ditto.Archetype.Tests
{
    using System.Linq;
    using global::Archetype.Models;
    using global::Archetype.PropertyConverters;
    using NUnit.Framework;

    [TestFixture]
    public class NestedArchetypeMappingTests
    {
        private ArchetypeModel _archetype;

        [TestFixtureSetUp]
        public void Init()
        {
            var archetypeJson = "{\"fieldsets\":[{\"properties\":[{\"alias\":\"name\",\"value\":\"Section 1\"},{\"alias\":\"content\",\"value\":\"{\\\"fieldsets\\\":[{\\\"properties\\\":[{\\\"alias\\\":\\\"contentType\\\",\\\"value\\\":\\\"Static\\\"}],\\\"alias\\\":\\\"section\\\",\\\"disabled\\\":false},{\\\"properties\\\":[{\\\"alias\\\":\\\"contentType\\\",\\\"value\\\":\\\"Dynamic\\\"}],\\\"alias\\\":\\\"section\\\",\\\"disabled\\\":false},{\\\"properties\\\":[{\\\"alias\\\":\\\"contentType\\\",\\\"value\\\":\\\"Branded\\\"}],\\\"alias\\\":\\\"section\\\",\\\"disabled\\\":false}]}\"}],\"alias\":\"sections\",\"disabled\":false}]}";
            var converter = new ArchetypeValueConverter();

            _archetype = (ArchetypeModel)converter.ConvertDataToSource(null, archetypeJson, false);
        }

        [Test]
        public void ArchetypeModel_Initialized()
        {
            Assert.IsNotNull(_archetype);
            Assert.IsNotEmpty(_archetype.Fieldsets);
        }

        public class MyModel
        {
            public string Name { get; set; }

            public ArchetypeModel Content { get; set; }
        }

        [Test]
        public void NestedArchetype_Mapped()
        {
            var fieldset = _archetype.Fieldsets.FirstOrDefault();
            var model = fieldset.As<MyModel>();

            Assert.IsNotNull(model);
            Assert.IsInstanceOf<MyModel>(model);
            Assert.That(model.Name, Is.EqualTo("Section 1"));

            // TODO: [LK] Need to work on this part, how to convert Archetype's JSON string into a strongly-typed `ArchetypeModel`?
            Assert.IsNull(model.Content);
        }
    }
}