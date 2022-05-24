global using ProjectTargetContextType = RheaWebsiteLatest.Service.RheaWebsiteLatestContext;

using Microsoft.EntityFrameworkCore;
using RheaWebsiteLatest.Service.Models;
using Vidyano.Service;

namespace RheaWebsiteLatest.Service
{
    public class RheaWebsiteLatestContext : TargetDbContext
    {
        public RheaWebsiteLatestContext(DbContextOptions<RheaWebsiteLatestContext> options) : base(options)
        {
        }
        public DbSet<PortalPage> PortalPages { get; set; }
        public DbSet<PortalMain> PortalMains { get; set; }
        public DbSet<PortalSection> PortalSections { get; set; }
        public DbSet<PortalVideo> PortalVideos { get; set; }
        public DbSet<PortalService> PortalServices { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
