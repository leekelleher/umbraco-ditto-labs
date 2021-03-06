﻿using System.Linq;
using Archetype.Models;
using Archetype.PropertyConverters;
using NUnit.Framework;

namespace Our.Umbraco.Ditto.Contrib.Tests.Archetype
{
    [TestFixture]
    public class FieldsetMappingTests
    {
        private ArchetypeModel _archetype;

        [TestFixtureSetUp]
        public void Init()
        {
            var archetypeJson = "{\"fieldsets\":[{\"properties\":[{\"alias\":\"textField\",\"value\":\"Testing text field\"}],\"alias\":\"myModel\",\"disabled\":false}]}";
            var converter = new ArchetypeValueConverter();

            _archetype = (ArchetypeModel)converter.ConvertDataToSource(null, archetypeJson, false);
        }

        public class MyModel
        {
            public string TextField { get; set; }
        }

        [Test]
        public void Fieldset_Model_Mapped()
        {
            var fieldset = _archetype.Fieldsets.FirstOrDefault();
            var model = fieldset.As<MyModel>();

            Assert.IsNotNull(model);
            Assert.IsInstanceOf<MyModel>(model);
            Assert.That(model.TextField, Is.EqualTo("Testing text field"));
        }
    }
}