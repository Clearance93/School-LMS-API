using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class LeadershipProgramRepository : GenericRepository<LeadershipProgram>, ILeadershipPropgramRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;
        public LeadershipProgramRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context; 
        }

        public async Task<IEnumerable<LeadershipProgram>> GetAllByOrganizationAndUserId(Guid organizationId, Guid userId)
        {
            return await _Context.LeadershipProgram.Where(d => d.OrganizationId == organizationId &&
                                                               d.AdminId == userId)
                                                   .ToListAsync();
        }
    }
}
