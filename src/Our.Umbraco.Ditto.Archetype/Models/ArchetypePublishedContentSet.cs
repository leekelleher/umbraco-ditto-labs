namespace Our.Umbraco.Ditto.Archetype
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using global::Archetype.Models;

    public class ArchetypePublishedContentSet : IEnumerable<ArchetypePublishedContent>
    {
        private IEnumerable<ArchetypePublishedContent> items { get; set; }

        public ArchetypePublishedContentSet(ArchetypeModel archetype)
        {
            items = archetype.Fieldsets.Select(x => new ArchetypePublishedContent(x));
        }

        public IEnumerator<ArchetypePublishedContent> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}