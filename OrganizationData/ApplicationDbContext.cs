using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrganizationModels;
using OrganizationModels.Model;
using OrganizationModels.Model.Communication;
using OrganizationModels.Model.Settings;

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

        public DbSet<SchoolAdminSettings> SchoolAdminSettings { get; set; }

        public DbSet<GradeStream> GradeStreams { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<Guests> Guests { get; set; }

        public DbSet<Learners> Learners { get; set; }

        public DbSet<Students> Students { get; set; }

        public DbSet<SchoolSubjects> SchoolSubjects { get; set; }

        public DbSet<CourseStreams> CourseStreams { get; set; }

        public DbSet<ExamGradeScale> ExamGradeScale { get; set; }

        public DbSet<LibraryItem> LibraryItem { get; set; }

        public DbSet<RegistrrationLink> RegistrrationLink { get; set; }

        public DbSet<Message> Message { get; set; }
    }
}
