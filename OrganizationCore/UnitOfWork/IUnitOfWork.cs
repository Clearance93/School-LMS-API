using OrganizationIInterface.IReporitory;
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

        Task<int> SaveChangeAsync();
    }
}
