using OrganizationModels.Model.Settings;

namespace OrganizationIInterface.IReporitory.Schools.Settings
{
    public interface IExamGradeScaleRepositoryInterface : IGenericRepository<ExamGradeScale>
    {
        Task<ExamGradeScale?> GetAllExamsGradesByOrganizationIdAsync(Guid id);
    }
}