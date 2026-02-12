using OrganizationModels.Model.Settings;

namespace OrganizationIInterface.IReporitory.Schools
{
    public interface IGradeRepositoryInterface : IGenericRepository<Grade>
    {
        Task<Grade?> GetOrganizationGrade(string gradeName);

        Task<IEnumerable<Grade>> GetAllGradesByOrganization(Guid organizationId);

        Task<Grade?> GetGradeByGradeStreamIdAsync(string gradeName);
    }
}
