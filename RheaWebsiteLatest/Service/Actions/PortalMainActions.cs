using RheaWebsiteLatest.Service.Models;
using Vidyano.Service.Repository;

namespace RheaWebsiteLatest.Service.Actions
{
    public class PortalMainActions : DefaultPersistentObjectActions<RheaWebsiteLatestContext, PortalMain>
    {
        public PortalMainActions(RheaWebsiteLatestContext context) : base(context)
        {
        }
    }
}
