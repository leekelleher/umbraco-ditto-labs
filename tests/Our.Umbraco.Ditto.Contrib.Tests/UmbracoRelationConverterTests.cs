namespace Our.Umbraco.Ditto.Contrib
{
	using System;
	using global::Umbraco.Core;
	using global::Umbraco.Core.Models;
	using NUnit.Framework;

	[TestFixture]
	public class UmbracoRelationConverterTests
	{
		[Test]
		public void UmbracoRelation_To_PublishedContent_Test()
		{
			var docGuid = Guid.Parse(Constants.ObjectTypes.Document);
			var relationType = new RelationType(docGuid, docGuid, "myRelationType");
			var relation = new Relation(1111, 2222, relationType);

			var converter = new UmbracoRelationConverter();

			Assert.That(converter.CanConvertFrom(relation.GetType()));

			var result = converter.ConvertFrom(relation);

			Assert.That(result, Is.Not.Null);
		}
	}
}