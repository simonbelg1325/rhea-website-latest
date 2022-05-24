using System.Reflection;
using Vidyano.Service;
using Vidyano.Service.Repository;

namespace RheaWebsiteLatest.Service;

public class DefaultPersistentObjectActions<TContext, TEntity> : PersistentObjectActionsReference<TContext, TEntity> where TContext : TargetDbContext where TEntity : class
{
    public DefaultPersistentObjectActions(TContext context) : base(context)
    {
    }

    private static readonly string CREATED_BY = "CreatedBy";
    private static readonly string CREATED_ON = "CreatedOn";
    private static readonly string CHANGED_BY = "ChangedBy";
    private static readonly string CHANGED_ON = "ChangedOn";

    protected override void PersistToContext(PersistentObject obj, TEntity entity)
    {
        TypeInfo typeInfo = typeof(TEntity).GetTypeInfo();
        foreach (var attribute in obj.Attributes)
        {
            if (attribute.Type == "Reference" && attribute.GetValue() == null)
            {
                var prop = typeInfo.DeclaredProperties.Where(it => it.Name == attribute.Name + "Id").SingleOrDefault();
                if (prop != null)
                {
                    prop.SetValue(entity, null);
                }
            }
        }

        base.PersistToContext(obj, entity);
    }

    public override void OnSave(PersistentObject obj)
    {
        if (obj.IsNew)
        {
            if (obj.TryGetAttribute(CREATED_BY, out var createdByProperty))
            {
                createdByProperty.SetValue(Manager.Current.UserName);
            }
            if (obj.TryGetAttribute(CREATED_ON, out var createdOnProperty))
            {
                createdOnProperty.SetValue(DateTime.Now);
            }

        }
        else
        {
            if (obj.TryGetAttribute(CHANGED_BY, out var changedByProperty))
            {
                changedByProperty.SetValue(Manager.Current.UserName);
            }
            if (obj.TryGetAttribute(CHANGED_ON, out var changedOnProperty))
            {
                changedOnProperty.SetValue(DateTime.Now);
            }
        }
        base.OnSave(obj);
    }

    public override void OnConstruct(PersistentObject obj)
    {
        if (obj.TryGetAttribute(CREATED_BY, out var createdByProperty))
        {
            createdByProperty.Visibility = AttributeVisibility.Never;
        }
        if (obj.TryGetAttribute(CREATED_ON, out var createdOnProperty))
        {
            createdOnProperty.Visibility = AttributeVisibility.Never;
        }

        if (obj.TryGetAttribute(CHANGED_BY, out var changedByProperty))
        {
            changedByProperty.Visibility = AttributeVisibility.Never;
        }
        if (obj.TryGetAttribute(CHANGED_ON, out var changedOnProperty))
        {
            changedOnProperty.Visibility = AttributeVisibility.Never;
        }
        base.OnConstruct(obj);
    }

    public override void OnConstruct(Query query, PersistentObject? parent)
    {
        base.OnConstruct(query, parent);

        if (query.PersistentObject.TryGetAttribute(CREATED_BY, out var createdByProperty))
        {
            createdByProperty.Visibility = AttributeVisibility.Never;
        }

        if (query.PersistentObject.TryGetAttribute(CREATED_ON, out var createdOnProperty))
        {
            createdOnProperty.Visibility = AttributeVisibility.Never;
        }

        if (query.PersistentObject.TryGetAttribute(CHANGED_BY, out var changedByProperty))
        {
            changedByProperty.Visibility = AttributeVisibility.Never;
        }

        if (query.PersistentObject.TryGetAttribute(CHANGED_ON, out var changedOnProperty))
        {
            changedOnProperty.Visibility = AttributeVisibility.Never;
        }
    }

    public override void OnDelete(PersistentObject? parent, IEnumerable<TEntity> entities, Query query, QueryResultItem[] selectedItems)
    {
        if (Manager.Current.RetryResult == null)
        {
            Manager.Current.RetryAction(Manager.Current.GetTranslatedMessage("AskForDeleteWithChildrenTitle"), Manager.Current.GetTranslatedMessage("AskForDeleteWithChildren"),
            options: new string[] { "Yes", "No" }, defaultOption: 1);
        }

        if (Manager.Current.RetryResult.Option == "Yes")
        {
            base.OnDelete(parent, entities, query, selectedItems);
        }
    }
}