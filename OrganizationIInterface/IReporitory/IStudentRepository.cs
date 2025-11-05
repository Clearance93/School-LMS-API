using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IStudentRepository : IGenericRepository<Students>
    {
        Task<Students?> GetStudentByEmailAsync(string email);
    }
}
