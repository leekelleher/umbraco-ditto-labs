namespace Our.Umbraco.Ditto.Archetype.Tests
{
    using System.Linq;
    using global::Archetype.Models;
    using global::Archetype.PropertyConverters;
    using global::Umbraco.Web;
    using NUnit.Framework;

    [TestFixture]
    public class ArchetypePropertyValueResolverTests
    {
        private ArchetypeModel _archetype;

        [TestFixtureSetUp]
        public void Init()
        {
            var archetypeJson = "{\"fieldsets\":[{\"properties\":[{\"alias\":\"content\",\"value\":\"{\\\"fieldsets\\\":[{\\\"properties\\\":[{\\\"alias\\\":\\\"contentType\\\",\\\"value\\\":\\\"Static\\\"}],\\\"alias\\\":\\\"section\\\",\\\"disabled\\\":false},{\\\"properties\\\":[{\\\"alias\\\":\\\"contentType\\\",\\\"value\\\":\\\"Dynamic\\\"}],\\\"alias\\\":\\\"section\\\",\\\"disabled\\\":false},{\\\"properties\\\":[{\\\"alias\\\":\\\"contentType\\\",\\\"value\\\":\\\"Branded\\\"}],\\\"alias\\\":\\\"section\\\",\\\"disabled\\\":false}]}\"}],\"alias\":\"sections\",\"disabled\":false}]}";
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
            // The idea here is to find a way to call either `ArchetypeFieldsetModel.GetValue<T>` or `ArchetypePropertyModel.GetValue<T>`.
            // If we use a ValueResolver, then we'll be un-plugging the default "UmbracoPropertyAttribute\UmbracoPropertyValueResolver" (which isn't so bad).
            // One downside of having a custom `MyArchetypePropertyValueResolver` is that we'd need to define it on every property, gah!
            // Unless there was a way we could set Ditto's default ValueResolver type? (maybe look at Umbraco's Resolver pattern?
            // https://our.umbraco.org/documentation/Reference/Plugins/creating-resolvers
            [DittoValueResolver(typeof(MyArchetypePropertyValueResolver))]
            public ArchetypeModel Content { get; set; }
        }

        public class MyArchetypePropertyValueResolver : DittoValueResolver
        {
            public override object ResolveValue()
            {
                object propertyValue = null;

                if (this.Content is ArchetypePublishedContent)
                {
                    var fieldset = ((ArchetypePublishedContent)this.Content).ArchetypeFieldset;

                    var method = fieldset
                        .GetType()
                        .GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                        .Where(x => x.Name == "GetValue" && x.GetGenericArguments().Length > 0)
                        .FirstOrDefault();

                    var generic = method.MakeGenericMethod(this.Context.PropertyDescriptor.PropertyType);

                    propertyValue = generic.Invoke(fieldset, new object[] { this.Context.PropertyDescriptor.Name });
                }

                return propertyValue;
            }
        }

        [Test]
        public void NestedArchetype_Mapped()
        {
            var fieldset = _archetype.Fieldsets.FirstOrDefault();
            var model = fieldset.As<MyModel>();

            Assert.IsNotNull(model);
            Assert.IsInstanceOf<MyModel>(model);

            // TODO: [LK] Need to work on this part, how to convert Archetype's JSON string into a strongly-typed `ArchetypeModel`?
            Assert.IsNull(model.Content);
        }
    }
}