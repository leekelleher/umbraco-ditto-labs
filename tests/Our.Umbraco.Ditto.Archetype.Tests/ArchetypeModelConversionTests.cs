namespace Our.Umbraco.Ditto.Archetype.Tests
{
    using System.Linq;
    using global::Archetype.Models;
    using global::Archetype.PropertyConverters;
    using global::Umbraco.Core.Models;
    using global::Umbraco.Web;
    using NUnit.Framework;

    [TestFixture]
    public class ArchetypeModelConversionTests
    {
        private ArchetypeModel _archetype;

        [TestFixtureSetUp]
        public void Init()
        {
            var archetypeJson = "{\"fieldsets\":[{\"properties\":[{\"alias\":\"textField\",\"value\":\"Testing text field\"}],\"alias\":\"myModel\",\"disabled\":false}]}";
            var converter = new ArchetypeValueConverter();

            _archetype = (ArchetypeModel)converter.ConvertDataToSource(null, archetypeJson, false);
        }

        [Test]
        public void ArchetypeModel_Initialized()
        {
            Assert.IsNotNull(_archetype);
            Assert.IsNotEmpty(_archetype.Fieldsets);
        }

        [Test]
        public void ArchetypeModel_To_PublishedContentSet()
        {
            var contentSet = _archetype.ToPublishedContentSet();

            Assert.That(contentSet, Is.Not.Null);
            Assert.That(contentSet, Is.Not.Empty);
            Assert.That(contentSet, Is.All.InstanceOf<IPublishedContent>());
        }

        [Test]
        public void ArchetypeFieldsetModel_To_PublishedContent()
        {
            var fieldset = _archetype.Fieldsets.FirstOrDefault();
            var content = fieldset.ToPublishedContent();

            Assert.That(content, Is.Not.Null);
            Assert.That(content, Is.InstanceOf<IPublishedContent>());
            Assert.That(content.Properties, Is.Not.Empty);
            Assert.That(content.Properties, Is.All.InstanceOf<IPublishedProperty>());

            var value = content.GetPropertyValue("textField");
            Assert.That(value, Is.EqualTo("Testing text field"));
        }
    }
}