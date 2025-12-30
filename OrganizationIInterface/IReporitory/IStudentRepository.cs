using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IStudentRepository : IGenericRepository<Students>
    {
        Task<Students?> GetStudentByEmailAsync(string email);

        Task<IEnumerable<Students>> GetAllOrganizationStudentsAsync(Guid organizationId);
    }
}
