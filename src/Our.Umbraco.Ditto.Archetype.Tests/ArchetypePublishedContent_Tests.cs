namespace Our.Umbraco.Ditto.Archetype.Tests
{
	using System.Linq;
	using global::Archetype.Models;
	using global::Umbraco.Core.Models;
	using global::Umbraco.Web;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class ArchetypePublishedContent_Tests
	{
		[Test]
		public void ArchetypePublishedContent_Model()
		{
			var archetypeJson = "{\"fieldsets\":[{\"properties\":[{\"alias\":\"textField\",\"value\":\"Testing text field\"}],\"alias\":\"myModel\",\"disabled\":false}]}";
			var archetype = JsonConvert.DeserializeObject<ArchetypeModel>(archetypeJson);

			Assert.IsNotNull(archetype);
			Assert.IsNotEmpty(archetype.Fieldsets);

			var contentSet = archetype.ToPublishedContentSet();

			Assert.That(contentSet, Is.Not.Null);
			Assert.That(contentSet, Is.Not.Empty);
			Assert.That(contentSet, Is.All.InstanceOf<IPublishedContent>());
		}

		[Test]
		public void ArchetypePublishedContent_Fieldset()
		{
			var archetypeJson = "{\"fieldsets\":[{\"properties\":[{\"alias\":\"textField\",\"value\":\"Testing text field\"}],\"alias\":\"myModel\",\"disabled\":false}]}";
			var archetype = JsonConvert.DeserializeObject<ArchetypeModel>(archetypeJson);

			Assert.IsNotNull(archetype);
			Assert.IsNotEmpty(archetype.Fieldsets);

			var fieldset = archetype.Fieldsets.FirstOrDefault();
			var content = fieldset.ToPublishedContent();

			Assert.That(content, Is.Not.Null);
			Assert.That(content, Is.InstanceOf<IPublishedContent>());
			Assert.That(content.Properties, Is.Not.Empty);

			var value = content.GetPropertyValue("textField");
			Assert.That(value, Is.EqualTo("Testing text field"));
		}
	}
}