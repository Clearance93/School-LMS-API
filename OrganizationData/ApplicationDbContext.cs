using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

        public DbSet<Event> Events { get; set; }

        public DbSet<Activities> Activities { get; set; }

        public DbSet<ClassSchedule> ClassSchedules { get; set; }

        public DbSet<StudentAttendance> StudentAttendances { get; set; }

        public DbSet<AttendanceSession> AttendanceSessions { get; set; }

        public DbSet<TeachingClass> TeachingClass { get; set; }

        public DbSet<AssignmentGrades> AssignmentGrades { get; set; }

        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }

        public DbSet<AttendanceOverview> AttendanceOverviews { get; set; }

        public DbSet<NewClass> NewClass { get; set; }

        public DbSet<ScheduledWorkshop> ScheduledWorkshop { get; set; }

        public DbSet<LeadershipProgram> LeadershipProgram { get; set; }

        public DbSet<TaskSubmission> TaskSubmission {  get; set; }

        public DbSet<StudentsGrade> StudentsGrade {  get; set; }

        public DbSet<AcademicProgress> AcademicProgress {  get; set; }

        public DbSet<StudentAttendanceOverview> StudentAttendanceOverview {  get; set; }

        public DbSet<BackToBackCommunication> BackToBackCommunication {  get; set; }

        public DbSet<AIMarkingResponse> AIMarkingResponse {  get; set; }

        public DbSet<AIMarkingRequest> AIMarkingRequest { get; set; }

        public DbSet<PlagiarismResults> PlagiarismResults { get; set; }

        public DbSet<PreRecordedVideo> PreRecordedVideo { get; set; }
    }
}
