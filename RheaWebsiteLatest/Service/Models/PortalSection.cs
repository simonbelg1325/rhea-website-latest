namespace RheaWebsiteLatest.Service.Models
{
    public class PortalSection : EntityBase
    {
        public bool LightBg { get; set; }
        public string? TopLine { get; set; }
        public string? Headline { get; set; }
        public string? Description { get; set; }
        public bool ImgStart { get; set; }
        public string? Image { get; set; }
        public string? Alt { get; set; }
        public string? VideoUrl { get; set; }
        public int Order { get; set; }
        public int PortalPageId { get; set; }
        public virtual PortalPage PortalPage { get; set; }
    }
}
