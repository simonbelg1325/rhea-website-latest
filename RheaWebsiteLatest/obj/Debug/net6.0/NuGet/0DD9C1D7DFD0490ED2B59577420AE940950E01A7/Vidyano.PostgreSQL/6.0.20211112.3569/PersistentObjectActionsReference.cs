//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from the Vidyano.PostgreSQL NuGet package.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the NuGet package is updated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vidyano.Core.Extensions;
using Vidyano.Service;
using Vidyano.Service.Repository;

namespace RheaWebsiteLatest.Service
{
    public partial class PersistentObjectActionsReference<TContext, TEntity> : PersistentObjectActions<TContext, TEntity>
        where TContext : ITargetDbContext
        where TEntity : class
    {
        #region Constructor

        public PersistentObjectActionsReference(TContext context) : base(context)
        {
        }

        #endregion

        #region Data Security

        /// <inheritdoc />
        protected override Source<TEntity> Where(Source<TEntity> source, Query? query = null)
        {
            return source;
        }

        /// <inheritdoc />
        public override TEntity? ApplyDataSecurity(TEntity entity, PersistentObject? obj = null, Query? query = null)
        {
            return Where(entity, query).FirstOrDefault();
        }

        #endregion

        #region Query

        /// <inheritdoc />
        public override void OnAddReference(PersistentObject parent, IEnumerable<TEntity> entities, Query query, QueryResultItem[] selectedItems)
        {
            // This method is called when a Query has defined the LookupSource and the end user uses the "Add" action to select items
            // Or when a developer uses the AddReference method on a Custom Action and the end user uses selects items

            if (Parameters != null && Parameters.TryGetValue("AddAction", out var addAction))
                throw new FaultException(string.Format("DEV: Invalid custom AddReference call, you must override the OnAddReference method in the PersistentObject actions class ({0}Actions) and handle '{1}'.", query.PersistentObject.Type, addAction));

            if (parent == null)
                throw new FaultException(string.Format("DEV: Invalid AddReference call, if this is a custom implementation you must override the OnAddReference method in the PersistentObject actions class ({0}Actions).", query.PersistentObject.Type));

            if (selectedItems != null)
            {
                var loadedParentEntity = Context.GetEntity(parent, true);
                entities.Run(e => loadedParentEntity.AddToChildCollection(e.GetType(), e, true, query.Source));
                PersistChanges();
            }
        }

        /// <inheritdoc />
        public override void OnDelete(PersistentObject? parent, IEnumerable<TEntity> entities, Query query, QueryResultItem[] selectedItems)
        {
            foreach (var entity in entities)
            {
                if (entity != null)
                    MarkForDeletion(entity);
            }

            PersistChanges();
        }

        /// <inheritdoc />
        public override void OnRemoveReference(PersistentObject parent, IEnumerable<TEntity> entities, Query query, QueryResultItem[] selectedItems)
        {
            if (selectedItems == null || selectedItems.Length == 0)
                throw new FaultException(Manager.Current.GetTranslatedMessage("NoItemsSelected"));
            if (parent == null)
                throw new FaultException("DEV: Need parent to execute Remove action.");

            var loadedParentEntity = Context.GetEntity(parent, true);
            if (loadedParentEntity != null)
                entities.Run(e => loadedParentEntity.RemoveFromChildCollection(e.GetType(), e, true, query.Source));
            PersistChanges();
        }

        #endregion

        #region Save

        /// <inheritdoc />
        public override void OnSave(PersistentObject obj)
        {
            if (obj.IsNew)
                SaveNew(obj);
            else
                SaveExisting(obj, LoadEntity(obj));

            obj.SaveDetails(Context, Parameters);

            PersistChanges();
        }

        /// <inheritdoc />
        protected override void SaveExisting(PersistentObject obj, TEntity? entity)
        {
            if (entity == null)
                throw new UnableToLoadEntityException(obj);

            UpdateEntity(obj, entity);

            if (CheckRules(obj, entity))
            {
                PersistToContext(obj, entity);

                PopulateAfterPersist(obj, entity);
            }
            else
                throw new SaveFailedException();
        }

        /// <inheritdoc />
        protected override void SaveNew(PersistentObject obj)
        {
            var newEntity = CreateNewEntity(obj);

            var parent = obj.Parent;
            if (parent != null)
                SetParentRelation(obj, parent, newEntity, GetEntityType(obj));

            UpdateEntity(obj, newEntity);

            if (CheckRules(obj, newEntity))
            {
                PersistToContext(obj, newEntity);

                PopulateAfterPersist(obj, newEntity);
            }
            else
                throw new SaveFailedException();
        }

        /// <inheritdoc />
        protected override void UpdateEntity(PersistentObject obj, TEntity entity)
        {
            if (entity == null)
                throw new UnableToLoadEntityException(obj);

            obj.PopulateObjectValues(entity, Context, obj.IsNew);
        }

        /// <inheritdoc />
        protected override void CheckRules(PersistentObject obj, Type entityType, TEntity? entity)
        {
            var ruleAttributes = obj.Attributes.Where(a => !a.IsReadOnly && (!string.IsNullOrEmpty(a.Rules) || a.Type == DataTypes.Image)).OrderBy(a => a.Offset).ToArray();

            var sb = new StringBuilder();
            CheckAttributeRules(entityType, entity, ruleAttributes, sb);
            if (sb.Length > 0)
                obj.AddNotification(sb.ToString(), NotificationType.Error);
        }

        /// <inheritdoc />
        public override void CheckDetailRules(PersistentObject parent, PersistentObjectAttributeAsDetail detailAttribute, PersistentObject[] objects)
        {
            foreach (var obj in objects)
            {
                CheckRules(obj);
            }
        }

        /// <inheritdoc />
        protected override void PersistToContext(PersistentObject obj, TEntity entity)
        {
            if (obj.IsNew)
                Context.AddObject(obj, entity);

            PersistChanges();
        }

        /// <inheritdoc />
        protected override void PopulateAfterPersist(PersistentObject obj, TEntity entity)
        {
            obj.UpdateKeyFromEntity(entity);

            if (!obj.IsInBulkEditMode)
                obj.PopulateAttributeValues(entity);
        }

        /// <inheritdoc />
        protected override void SetParentRelation(PersistentObject obj, PersistentObject parent, TEntity entity, Type itemType)
        {
            var parentInstance = Context.GetEntity(parent);
            if (parentInstance != null)
                parentInstance.AddToChildCollection(itemType, entity, false);
        }

        #endregion

        #region Load

        /// <inheritdoc />
        protected override TEntity? LoadEntity(PersistentObject obj, bool forRefresh = false)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            TEntity? loadEntity = null;
            if (obj.IsNew)
            {
                if (forRefresh)
                    loadEntity = CreateNewEntity(obj);
            }
            else
            {
                loadEntity = GetEntity(obj);

                if (loadEntity != null)
                    loadEntity = ApplyDataSecurity(loadEntity, obj);
            }

            if (loadEntity != null && forRefresh)
                obj.PopulateObjectValues(loadEntity, Context, false);
            return loadEntity;
        }

        /// <inheritdoc />
        public override TEntity? GetEntity(PersistentObject obj)
        {
            return Context.GetEntity<TEntity>(obj);
        }

        #endregion

        #region New

        /// <inheritdoc />
        protected override PersistentObjectAttributeWithReference? GetParentAttributeForNew(PersistentObject obj, PersistentObject parent)
        {
            if (parent.IsNew)
                return null;

            var refAttrs = obj.Attributes.OfType<PersistentObjectAttributeWithReference>().Where(a => a.Lookup!.PersistentObject.Id == parent.Id).ToArray();
            return refAttrs.Length == 1 ? refAttrs[0] : null;
        }

        #endregion

        #region Bulk Edit

        /// <inheritdoc />
        protected override void OnBulkSave(BulkSaveArgs e)
        {
            var obj = e.PersistentObject;
            obj.Attributes = e.ChangedAttributes;

            foreach (var id in e.BulkObjectIds)
            {
                obj.ObjectId = id;
                OnSave(obj);

                if (obj.HasError)
                    break;
            }
        }

        #endregion

        #region Context Related

        /// <inheritdoc />
        protected override void PersistChanges()
        {
            Context.SaveChanges();
        }

        /// <inheritdoc />
        protected override void MarkForDeletion(TEntity entity)
        {
            Context.MarkForDeletion(entity);
        }

        #endregion
    }
}