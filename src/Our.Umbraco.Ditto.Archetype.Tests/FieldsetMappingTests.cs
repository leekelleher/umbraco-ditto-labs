namespace Our.Umbraco.Ditto.Archetype.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using global::Archetype.Models;
    using Newtonsoft.Json;
    using NUnit.Framework;

    [TestFixture]
    public class FieldsetMappingTests
    {
        public class MyModel
        {
            public string TextField { get; set; }
        }

        [Test]
        public void Fieldset_Model_Mapped()
        {
            var archetypeJson = "{\"fieldsets\":[{\"properties\":[{\"alias\":\"textField\",\"value\":\"Testing text field\"}],\"alias\":\"myModel\",\"disabled\":false}]}";
            var archetype = JsonConvert.DeserializeObject<ArchetypeModel>(archetypeJson);

            Assert.IsNotNull(archetype);
            Assert.IsNotEmpty(archetype.Fieldsets);

            var fieldset = archetype.Fieldsets.FirstOrDefault();
            var model = fieldset.As<MyModel>(archetype: archetype);

            Assert.IsNotNull(model);
            Assert.IsInstanceOf<MyModel>(model);
            Assert.That(model.TextField, Is.EqualTo("Testing text field"));
        }
    }
}