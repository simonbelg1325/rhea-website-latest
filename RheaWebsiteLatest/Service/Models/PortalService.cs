namespace RheaWebsiteLatest.Service.Models
{
    public class PortalService : EntityBase
    {
        public string Image { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }  
        public int PortalPageId { get; set; }
        public virtual PortalPage PortalPage { get; set; }
    }
}
