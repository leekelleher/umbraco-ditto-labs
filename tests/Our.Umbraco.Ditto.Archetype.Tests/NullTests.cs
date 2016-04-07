using Archetype.Models;
using NUnit.Framework;

namespace Our.Umbraco.Ditto.Archetype.Tests
{
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