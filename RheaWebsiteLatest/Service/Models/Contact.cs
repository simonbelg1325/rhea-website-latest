namespace RheaWebsiteLatest.Service.Models
{
    public class Contact : EntityBase
    {
        public string Address { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; } 
        public string Map { get; set; }
        public int PortalPageId { get; set; }
        public virtual PortalPage PortalPage { get; set; }
    }
}
