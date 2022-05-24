using System;
using System.ComponentModel.DataAnnotations;

namespace RheaWebsiteLatest.Service.Models
{
    public class PortalPage : EntityBase
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<PortalSection> PortalSections { get; set; }
        public virtual ICollection<PortalMain> PortalMains { get; set; }
        public virtual ICollection<PortalService> PortalServices { get; set; }
        public virtual ICollection<Reference> References { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}

