using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Our.Umbraco.Ditto.Mocks;

namespace Our.Umbraco.Ditto.Contrib
{
    [TestFixture]
    public class AggregateValuesTests
    {
        const string PropertyNameAlias = "myProperty";

        public class MyModel
        {
            [AggregateValues]
            public IEnumerable<string> MyProperty { get; set; }

            [AggregateValues(PropertyName = PropertyNameAlias, ReverseOrder = true)]
            public IEnumerable<string> MyPropertyReversed { get; set; }
        }

        private MockPublishedContent Content;

        private List<string> Values;

        [TestFixtureSetUp]
        public void Init()
        {
            /*
             * This test will attempt to aggregate the recursive values of a property.
             * Taking the value from the current content node, then moving upwards through the content tree.
             * The unit-test is to check that the values set-up here are mapped accordingly in the Ditto processor.
             * An additional option of `ReverseOrder` is used to reverse the order of the list values.
             */

            this.Values = new List<string>() { "hello", "world", "foo" };

            var level1 = new MockPublishedContent
            {
                Properties = new[] { new MockPublishedProperty(PropertyNameAlias, this.Values[0], true) }
            };

            var level2 = new MockPublishedContent
            {
                Parent = level1,
                Properties = new[] { new MockPublishedProperty(PropertyNameAlias, this.Values[1], true) }
            };

            var level3 = new MockPublishedContent
            {
                Parent = level2,
                Properties = new[] { new MockPublishedProperty(PropertyNameAlias, this.Values[2], true) }
            };

            this.Content = level3;
        }

        [Test]
        public void AggregateValues_Test()
        {
            var model = this.Content.As<MyModel>();

            Assert.That(model, Is.Not.Null);
            Assert.That(model.MyProperty, Is.Not.Null);
            Assert.That(model.MyPropertyReversed, Is.Not.Null);

            // Make the value as a list, so it can be evaluated.
            var list = model.MyProperty.ToList();

            Assert.That(list, Has.Count);
            Assert.That(list.Count, Is.EqualTo(this.Values.Count));

            // The processor collects the values from the bottom-up,
            // they will be in the opposite order to our initial values,
            // so they will not be "equal", but they are "equivalent".
            CollectionAssert.AreNotEqual(list, this.Values);
            CollectionAssert.AreEquivalent(list, this.Values);

            // Make the value as a list, so it can be evaluated.
            var reversed = model.MyPropertyReversed.ToList();

            Assert.That(reversed, Has.Count);
            Assert.That(reversed.Count, Is.EqualTo(this.Values.Count));

            // The reversed order of the values should match the order of our initial values.
            CollectionAssert.AreEqual(reversed, this.Values);
        }
    }
}