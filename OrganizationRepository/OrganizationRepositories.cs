using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class OrganizationRepositories : GenericRepository<OrganizationSetup>, IOrganizationRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;

        public OrganizationRepositories(ApplicationDbContext context) : base(context) 
        {
            _Context = context;
        }

        public async Task<IEnumerable<OrganizationSetup>> GetAllOrganizationAsync()
        {
            return await _Context.OrganizationSetup.Where(org => org.IsActive == true && 
                                                                 org.IsDeleted != true)
                                                  .ToListAsync();
        }

        public async Task<OrganizationSetup?> GetOrganizationAsync(Guid id)
        {
            return await _Context.OrganizationSetup.FirstOrDefaultAsync(org => org.OrganizationSetupId == id);
        }

        public async Task<OrganizationSetup?> GetOrganizationByEmailAsync(string email)
        {
            var existingOrganization = await _Context.OrganizationSetup.Where(org => org.AdminEmail == email)
                                                                       .Select(org => new OrganizationSetup
                                                                       {
                                                                           OrganizationSetupId = org.OrganizationSetupId
                                                                       })
                                                                       .FirstOrDefaultAsync();

            return existingOrganization;
        }
    }
}
