namespace Our.Umbraco.Ditto.Archetype.Tests
{
    using System.ComponentModel;
    using System.Linq;
    using global::Archetype.Models;
    using Newtonsoft.Json;
    using NUnit.Framework;

    [TestFixture]
    public class NestedArchetypeMappingTests
    {
        private ArchetypeModel _archetype;

        [TestFixtureSetUp]
        public void Init()
        {
            var archetypeJson = "{\"fieldsets\":[{\"properties\":[{\"alias\":\"name\",\"value\":\"Section 1\"},{\"alias\":\"content\",\"value\":\"{\\\"fieldsets\\\":[{\\\"properties\\\":[{\\\"alias\\\":\\\"contentType\\\",\\\"value\\\":\\\"Static\\\"}],\\\"alias\\\":\\\"section\\\",\\\"disabled\\\":false},{\\\"properties\\\":[{\\\"alias\\\":\\\"contentType\\\",\\\"value\\\":\\\"Dynamic\\\"}],\\\"alias\\\":\\\"section\\\",\\\"disabled\\\":false},{\\\"properties\\\":[{\\\"alias\\\":\\\"contentType\\\",\\\"value\\\":\\\"Branded\\\"}],\\\"alias\\\":\\\"section\\\",\\\"disabled\\\":false}]}\"}],\"alias\":\"sections\",\"disabled\":false}]}";

            _archetype = JsonConvert.DeserializeObject<ArchetypeModel>(archetypeJson);
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

            //[TypeConverter(typeof(DittoArchetypeConverter))]
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