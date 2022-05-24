using System;
using System.ComponentModel.DataAnnotations;

namespace RheaWebsiteLatest.Service.Models
{
    public class EntityBase
    {
        public EntityBase()
        {
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [MaxLength(255)]
        public string? ChangedBy { get; set; }
        public DateTime? ChangedOn { get; set; }
    }
}

