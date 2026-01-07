using OrganizationModels;
using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IActivitiesRepositoryInertface : IGenericRepository<Activities>
    {
        Task<IEnumerable<Activities>> GetAllActivitiesByOrganization(Guid organizationId);

        Task<ApplicationUser?> GetUserByEmail(string email);
    }
}
