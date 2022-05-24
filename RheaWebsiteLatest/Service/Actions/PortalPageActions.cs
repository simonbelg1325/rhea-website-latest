using System;
using System.ComponentModel.DataAnnotations;
using RheaWebsiteLatest.Service.Models;
using Vidyano.Core.Extensions;
using Vidyano.Service.Repository;

namespace RheaWebsiteLatest.Service.Actions
{
    public class PortalPageActions : DefaultPersistentObjectActions<RheaWebsiteLatestContext, PortalPage>
    {
        public PortalPageActions(RheaWebsiteLatestContext context) : base(context)
        {
        }

        public override void OnConstruct(PersistentObject obj)
        {
            base.OnConstruct(obj);

            obj.Queries.Run(q =>
            {
                q.PageSize = 0;
                q.IsIncludedInParentObject = true;
            });
        }
    }
}

