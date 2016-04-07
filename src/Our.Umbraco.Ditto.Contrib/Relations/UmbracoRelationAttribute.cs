using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Our.Umbraco.Ditto
{
    // TODO: [LK] Consider using `DittoMultiProcessor` to wrap this processor and the `UmbracoPicker` one,
    // saves trying to handle the responsibility of `int` => `IPublishedContent`

    [DittoProcessorMetaData(ContextType = typeof(UmbracoRelationProcessorContext), ValueType = typeof(IPublishedContent))]
    public class UmbracoRelationAttribute : DittoProcessorAttribute
    {
        public UmbracoRelationAttribute()
            : this(string.Empty, RelationDirection.ChildToParent)
        { }

        public UmbracoRelationAttribute(string relationTypeAlias, RelationDirection relationDirection = RelationDirection.ChildToParent)
        {
            this.RelationDirection = relationDirection;
            this.RelationTypeAlias = relationTypeAlias;
        }

        public RelationDirection RelationDirection { get; set; }

        public string RelationTypeAlias { get; set; }

        public override object ProcessValue()
        {
            var ctx = Context as UmbracoRelationProcessorContext;

            if (ctx == null || ctx.Content == null || ctx.RelationService == null || ctx.GetById == null)
            {
                return Enumerable.Empty<IRelation>();
            }

            IEnumerable<IRelation> relations;

            switch (this.RelationDirection)
            {
                case RelationDirection.ChildToParent:
                    relations = ctx.RelationService.GetByChildId(ctx.Content.Id);
                    break;

                case RelationDirection.ParentToChild:
                    relations = ctx.RelationService.GetByParentId(ctx.Content.Id);
                    break;

                case RelationDirection.Bidirectional:
                default:
                    relations = ctx.RelationService.GetByParentOrChildId(ctx.Content.Id);
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
                        .Select(x => x.ParentId)
                        .Select(ctx.GetById);

                case RelationDirection.ParentToChild:
                    return relations
                        .Select(x => x.ChildId)
                        .Select(ctx.GetById);

                case RelationDirection.Bidirectional:
                default:
                    return relations
                        .Select(x => x.ParentId)
                        .Union(relations.Select(x => x.ChildId))
                        .Where(x => ctx.Content.Id != x)
                        .Select(ctx.GetById);
            }
        }
    }
}