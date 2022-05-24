using RheaWebsiteLatest.Service.Models;

namespace RheaWebsiteLatest.Service.Actions
{
    public class PortalServiceActions : DefaultPersistentObjectActions<RheaWebsiteLatestContext, PortalService>
    {
        public PortalServiceActions(RheaWebsiteLatestContext context) : base(context)
        {
        }
    }
}
