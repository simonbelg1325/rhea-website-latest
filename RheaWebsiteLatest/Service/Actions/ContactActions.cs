using RheaWebsiteLatest.Service.Models;

namespace RheaWebsiteLatest.Service.Actions
{
    public class ContactActions : DefaultPersistentObjectActions<RheaWebsiteLatestContext, Contact>
    {
        public ContactActions(RheaWebsiteLatestContext context) : base(context)
        {
        }
    }
}
