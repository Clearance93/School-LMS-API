using OrganizationModels.Model.Settings;

namespace OrganizationIInterface.IReporitory.Schools.Settings
{
    public interface ISchoolSubjectRepositoryInterface : IGenericRepository<SchoolSubjects>
    {
        Task<SchoolSubjects?> GetSubjectBySubjectNameAsyc(Guid gradeId, string subjectName);
    }
}
