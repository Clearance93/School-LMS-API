using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrganizationModels;
using OrganizationModels.Model;

namespace OrganizationData
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<OrganizationSetup> OrganizationSetup { get; set; }

        public DbSet<Admins> Admins { get; set; }

        public DbSet<Teachers> Teachers { get; set; }

        public DbSet<StuffMembers> StuffMembers { get; set; }

        public DbSet<Guests> Guests { get; set; }

        public DbSet<Learners> Learners { get; set; }

        public DbSet<Students> Students { get; set; }
    }
}
