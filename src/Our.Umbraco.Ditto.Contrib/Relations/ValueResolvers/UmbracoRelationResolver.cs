namespace Our.Umbraco.Ditto
{
    using System.Collections.Generic;
    using System.Linq;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Models;

    public class UmbracoRelationResolver : DittoValueResolver<DittoValueResolverContext, UmbracoRelationAttribute>
    {
        public override object ResolveValue()
        {
            if (this.Content == null)
            {
                return Enumerable.Empty<IRelation>();
            }

            IEnumerable<IRelation> relations;

            switch (this.Attribute.RelationDirection)
            {
                case RelationDirection.ChildToParent:
                    relations = ApplicationContext.Current.Services.RelationService.GetByChildId(this.Content.Id);
                    break;

                case RelationDirection.ParentToChild:
                    relations = ApplicationContext.Current.Services.RelationService.GetByParentId(this.Content.Id);
                    break;

                case RelationDirection.Bidirectional:
                default:
                    relations = ApplicationContext.Current.Services.RelationService.GetByParentOrChildId(this.Content.Id);
                    break;
            }

            if (!string.IsNullOrWhiteSpace(this.Attribute.RelationTypeAlias))
            {
                relations = relations.Where(x => x.RelationType.Alias.InvariantEquals(this.Attribute.RelationTypeAlias));
            }

            return relations;
        }
    }
}