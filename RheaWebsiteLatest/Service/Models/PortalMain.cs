namespace RheaWebsiteLatest.Service.Models
{
    public class PortalMain : EntityBase
    {
        public string? NavSectionHeader { get; set; }
        public string? NavSectionTitle { get; set; }
        public int PortalVideoId { get; set; }
        public virtual PortalVideo PortalVideo { get; set; }
        public int PortalPageId { get; set; }
        public virtual PortalPage PortalPage { get; set; }
    }
}
