using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class ActivitiesRepository : GenericRepository<Activities>, IActivitiesRepositoryInertface
    {
        private readonly ApplicationDbContext _Context;
        public ActivitiesRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Activities>> GetAllActivitiesByOrganization(Guid organizationId)
        {
            return await _Context.Activities.Where(d => d.OrganizationId == organizationId)
                                            .ToListAsync();
        }

        public async Task<ApplicationUser?> GetUserByEmail(string email)
        {
            return await _Context.ApplicationUsers.FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}