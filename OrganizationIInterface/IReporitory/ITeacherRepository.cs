using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface ITeacherRepository : IGenericRepository<Teachers> 
    {
        Task<Teachers?> GetTeacherByEmailAsync(string email);
    }
}
