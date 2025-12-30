using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface ITeacherRepository : IGenericRepository<Teachers> 
    {
        Task<Teachers?> GetTeacherByEmailAsync(string email);

        Task<IEnumerable<Teachers?>> GetAllOrganizationTeachersAsync(Guid id);
    }
}
