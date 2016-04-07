using System;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;

namespace Our.Umbraco.Ditto.Contrib.Tests.Mocks
{
    public class MockRelationService : IRelationService
    {
        public bool AreRelated(IUmbracoEntity parent, IUmbracoEntity child)
        {
            throw new NotImplementedException();
        }

        public bool AreRelated(int parentId, int childId)
        {
            throw new NotImplementedException();
        }

        public bool AreRelated(int parentId, int childId, string relationTypeAlias)
        {
            throw new NotImplementedException();
        }

        public bool AreRelated(IUmbracoEntity parent, IUmbracoEntity child, string relationTypeAlias)
        {
            throw new NotImplementedException();
        }

        public void Delete(IRelationType relationType)
        {
            throw new NotImplementedException();
        }

        public void Delete(IRelation relation)
        {
            throw new NotImplementedException();
        }

        public void DeleteRelationsOfType(IRelationType relationType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRelation> GetAllRelations(params int[] ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRelation> GetAllRelationsByRelationType(int relationTypeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRelation> GetAllRelationsByRelationType(RelationType relationType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRelationType> GetAllRelationTypes(params int[] ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRelation> GetByChild(IUmbracoEntity child)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRelation> GetByChild(IUmbracoEntity child, string relationTypeAlias)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRelation> GetByChildId(int id)
        {
            var relationType = GetRelationTypeByAlias("myRelationType");

            yield return new Relation(9001, id, relationType);
            yield return new Relation(9002, id, relationType);
            yield return new Relation(9003, id, relationType);
        }

        public IRelation GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRelation> GetByParent(IUmbracoEntity parent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRelation> GetByParent(IUmbracoEntity parent, string relationTypeAlias)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRelation> GetByParentId(int id)
        {
            var relationType = GetRelationTypeByAlias("myRelationType");

            yield return new Relation(id, 8001, relationType);
            yield return new Relation(id, 8002, relationType);
            yield return new Relation(id, 8003, relationType);
        }

        public IEnumerable<IRelation> GetByParentOrChildId(int id)
        {
            var relationType = GetRelationTypeByAlias("myRelationType");

            yield return new Relation(9001, 8001, relationType);
            yield return new Relation(9002, 8002, relationType);
            yield return new Relation(9003, 8003, relationType);
        }

        public IEnumerable<IRelation> GetByRelationTypeAlias(string relationTypeAlias)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRelation> GetByRelationTypeId(int relationTypeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRelation> GetByRelationTypeName(string relationTypeName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUmbracoEntity> GetChildEntitiesFromRelations(IEnumerable<IRelation> relations, bool loadBaseType = false)
        {
            throw new NotImplementedException();
        }

        public IUmbracoEntity GetChildEntityFromRelation(IRelation relation, bool loadBaseType = false)
        {
            throw new NotImplementedException();
        }

        public Tuple<IUmbracoEntity, IUmbracoEntity> GetEntitiesFromRelation(IRelation relation, bool loadBaseType = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<IUmbracoEntity, IUmbracoEntity>> GetEntitiesFromRelations(IEnumerable<IRelation> relations, bool loadBaseType = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUmbracoEntity> GetParentEntitiesFromRelations(IEnumerable<IRelation> relations, bool loadBaseType = false)
        {
            throw new NotImplementedException();
        }

        public IUmbracoEntity GetParentEntityFromRelation(IRelation relation, bool loadBaseType = false)
        {
            throw new NotImplementedException();
        }

        public IRelationType GetRelationTypeByAlias(string alias)
        {
            var docGuid = Guid.Parse(Constants.ObjectTypes.Document);
            return new RelationType(docGuid, docGuid, alias);
        }

        public IRelationType GetRelationTypeById(int id)
        {
            throw new NotImplementedException();
        }

        public bool HasRelations(IRelationType relationType)
        {
            throw new NotImplementedException();
        }

        public bool IsRelated(int id)
        {
            throw new NotImplementedException();
        }

        public IRelation Relate(IUmbracoEntity parent, IUmbracoEntity child, string relationTypeAlias)
        {
            throw new NotImplementedException();
        }

        public IRelation Relate(IUmbracoEntity parent, IUmbracoEntity child, IRelationType relationType)
        {
            throw new NotImplementedException();
        }

        public void Save(IRelationType relationType)
        {
            throw new NotImplementedException();
        }

        public void Save(IRelation relation)
        {
            throw new NotImplementedException();
        }
    }
}