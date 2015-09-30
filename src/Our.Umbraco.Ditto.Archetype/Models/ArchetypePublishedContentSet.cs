namespace Our.Umbraco.Ditto
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Archetype.Models;

    public class ArchetypePublishedContentSet : IEnumerable<ArchetypePublishedContent>
    {
        private IEnumerable<ArchetypePublishedContent> items { get; set; }

        public ArchetypePublishedContentSet(ArchetypeModel archetype)
        {
            this.ArchetypeModel = archetype;

            items = archetype.Fieldsets
                .Where(x => x.Disabled == false)
                .Select(x => new ArchetypePublishedContent(x));
        }

        internal ArchetypeModel ArchetypeModel { get; private set; }

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