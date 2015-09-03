namespace Our.Umbraco.Ditto.Archetype.Tests
{
    using global::Archetype.Models;
    using NUnit.Framework;

    [TestFixture]
    public class NullTests
    {
        public class MyModel { }

        [Test]
        public void Null_Returns_Default()
        {
            var archetype = default(ArchetypeModel);

            var model = archetype.As<MyModel>();

            Assert.IsNull(model);
        }
    }
}