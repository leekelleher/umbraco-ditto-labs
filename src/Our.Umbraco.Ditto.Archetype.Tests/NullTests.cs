namespace Our.Umbraco.Ditto.Archetype.Tests
{
    using global::Archetype.Models;
    using NUnit.Framework;

    [TestFixture]
    public class NullTests
    {
        public class MyModel { }

        [Test]
        public void Null_Returns_Default_ArchetypeModel()
        {
            ArchetypeModel archetype = null;

            var model = archetype.As<MyModel>();

            Assert.IsNull(model);
        }

        [Test]
        public void Null_Returns_Default_ArchetypeFieldsetModel()
        {
            ArchetypeFieldsetModel fieldset = null;

            var model = fieldset.As<MyModel>();

            Assert.IsNull(model);
        }
    }
}