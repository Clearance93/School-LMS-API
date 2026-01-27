using OrganizationIInterface.IReporitory;
using OrganizationIInterface.IReporitory.Assignments;
using OrganizationIInterface.IReporitory.Schools;
using OrganizationIInterface.IReporitory.Schools.Settings;

namespace OrganizationCore.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUSerRepositoryInterface Users { get; }

        IOrganizationRepositoryInterface Organization { get; }

        IAdminRepositoryInterface Admin { get; }

        ILearnerrepositoryInterface Learner { get; }

        IStudentRepository Student { get; }

        IGuestsRepositoryInterface Guests { get; }

        ITeacherRepository Teacher { get; }

        IStuffMemberRepositoryInterface StuffMember { get; }

        ISchoolAdminSettingsrepositoryInterface SchoolAdminSettings { get; }

        IGradeRepositoryInterface Grade {  get; }

        IGradeStreamRepositoryInterface GradeStream { get; }

        ICourseStreamRepositoryInterface CourseStream { get; }

        ISchoolSubjectRepositoryInterface SchoolSubject { get; }

        IExamGradeScaleRepositoryInterface ExamGrade { get; }

        ILibraryInterfaceRepository Library { get; }

        IRegistrationLinkRepositoryInterface RegistrationLink { get; }

        ICommunicationMessageInterfaceRepository Communication {  get; }

        IEventInterfaceRerpository Events { get; }

        IActivitiesRepositoryInertface Activities { get; }

        IClassScheduleInterfaceRepository ClassSchedule { get; }

        IStudentAttendanceInterfaceRepository StudentAttendance { get; }

        IAttendanceSessionInterfaceRepository AttendanceSession { get; }

        ITeachingClassInterfaceRepository TeachingClass { get; }

        IAssingmentGradesRepositoryInterface AssignmentGrades { get; }

        IAssignmentSubmissionRepositoryInterface AssignmentSubmission { get; }

        IAssignmentRepositoryInterface Assignments { get; }

        IAttendanceOverViewRepositoryInterface AttendanceOverview { get; }

        ITeacherDashboardViewRepositoryInterface TeacherDashboard { get; }

        ISchoolDashboardRepositoryInterface SchoolDashboard { get; }

        INewClassRepositoryInterface NewClass { get; }

        IClassPerformanceViewRepositoryInterface ClassPerformance { get; }

        IAttendanceDailyToMonthlyRepositoryInterface MonthlyAttendance { get; }

        IPasswordChangeRepositoryInterface ChangePassword { get; }

        IScheduledWorkRpositoryInterface ScheduledWorkshop { get; }

        ILeadershipPropgramRepositoryInterface LeadershipPropgram { get; }

        Task<int> SaveChangeAsync();
    }
}
