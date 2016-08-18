using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Our.Umbraco.Ditto
{
    [DittoProcessorMetaData(ValueType = typeof(IPublishedContent))]
    internal class UmbracoRelationProcessorAttribute : DittoProcessorAttribute
    {
        public UmbracoRelationProcessorAttribute()
            : this(string.Empty, RelationDirection.ChildToParent)
        { }

        public UmbracoRelationProcessorAttribute(
            string relationTypeAlias,
            RelationDirection relationDirection = RelationDirection.ChildToParent)
        {
            this.RelationDirection = relationDirection;
            this.RelationTypeAlias = relationTypeAlias;
        }

        public RelationDirection RelationDirection { get; set; }

        public string RelationTypeAlias { get; set; }

        public override object ProcessValue()
        {
            if (Value == null || Context == null || Context.Content == null)
            {
                return Enumerable.Empty<IRelation>();
            }

            var content = this.Value as IPublishedContent ?? Context.Content;

            IEnumerable<IRelation> relations;

            switch (this.RelationDirection)
            {
                case RelationDirection.ChildToParent:
                    relations = ApplicationContext.Current.Services.RelationService.GetByChildId(content.Id);
                    break;

                case RelationDirection.ParentToChild:
                    relations = ApplicationContext.Current.Services.RelationService.GetByParentId(content.Id);
                    break;

                case RelationDirection.Bidirectional:
                default:
                    relations = ApplicationContext.Current.Services.RelationService.GetByParentOrChildId(content.Id);
                    break;
            }

            if (!string.IsNullOrWhiteSpace(this.RelationTypeAlias))
            {
                relations = relations.Where(x => x.RelationType.Alias.InvariantEquals(this.RelationTypeAlias));
            }

            switch (this.RelationDirection)
            {
                case RelationDirection.ChildToParent:
                    return relations
                        .Select(x => x.ParentId);

                case RelationDirection.ParentToChild:
                    return relations
                        .Select(x => x.ChildId);

                case RelationDirection.Bidirectional:
                default:
                    return relations
                        .Select(x => x.ParentId)
                        .Union(relations.Select(x => x.ChildId))
                        .Where(x => content.Id != x);
            }
        }
    }
}