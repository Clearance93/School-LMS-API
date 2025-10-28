using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IOrganizationRepositoryInterface : IGenericRepository<OrganizationSetup>
    {
        Task<IEnumerable<OrganizationSetup>> GetAllOrganizationAsync();

        Task<OrganizationSetup> GetOrganizationAsync(Guid id);

        Task<OrganizationSetup> GetOrganizationByEmailAsync(string email);
    }
}
