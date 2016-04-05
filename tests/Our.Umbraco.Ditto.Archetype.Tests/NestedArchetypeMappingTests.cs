namespace Our.Umbraco.Ditto.Archetype.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using global::Archetype.Models;
    using global::Archetype.PropertyConverters;
    using NUnit.Framework;

    [TestFixture]
    public class NestedArchetypeMappingTests
    {
        private ArchetypeModel _archetype;
        private ArchetypeModel _nestedArchetype;

        [TestFixtureSetUp]
        public void Init()
        {
            var converter = new ArchetypeValueConverter();

            var archetypeJson = "{\"fieldsets\":[{\"properties\":[{\"alias\":\"name\",\"value\":\"Section 1\"},{\"alias\":\"content\",\"value\":\"\"}],\"alias\":\"sections\",\"disabled\":false}]}";
            _archetype = (ArchetypeModel)converter.ConvertDataToSource(null, archetypeJson, false);

            // HACK: [LK] Due to internal workings of Archetype, it is difficult to contruct a nested Archetype model
            // from a serialized JSON string. We have to revert to taking a manual ceremony.
            var nestedArchetypeJson = "{\"fieldsets\":[{\"properties\":[{\"alias\":\"contentType\",\"value\":\"Static\"}],\"alias\":\"section\",\"disabled\":false},{\"properties\":[{\"alias\":\"contentType\",\"value\":\"Dynamic\"}],\"alias\":\"section\",\"disabled\":false},{\"properties\":[{\"alias\":\"contentType\",\"value\":\"Branded\"}],\"alias\":\"section\",\"disabled\":false}]}";
            _nestedArchetype = (ArchetypeModel)converter.ConvertDataToSource(null, nestedArchetypeJson, false);
            _archetype.First().Properties.First(x => x.Alias == "content").Value = _nestedArchetype;
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

            [DittoArchetype]
            public IEnumerable<MyNestedModel> Content { get; set; }
        }

        public class MyNestedModel
        {
            public string ContentType { get; set; }
        }

        [Test]
        public void NestedArchetype_Mapped()
        {
            var fieldset = _archetype.Fieldsets.FirstOrDefault();
            var model = fieldset.As<MyModel>();

            Assert.IsNotNull(model);
            Assert.IsInstanceOf<MyModel>(model);
            Assert.That(model.Name, Is.EqualTo("Section 1"));

            Assert.IsNotNull(model.Content);
            Assert.IsInstanceOf<IEnumerable<MyNestedModel>>(model.Content);

            var nestedFirst = model.Content.First();
            Assert.That(nestedFirst.ContentType, Is.EqualTo("Static"));
        }
    }
}