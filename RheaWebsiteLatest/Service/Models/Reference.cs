namespace RheaWebsiteLatest.Service.Models
{
    public class Reference: EntityBase
    {
        public string Image { get; set; }
        public string Alt { get; set; }
        public int Order { get; set; }
        public int PortalPageId { get; set; }
        public virtual PortalPage PortalPage { get; set; }
    }
}
