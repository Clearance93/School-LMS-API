using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class StuffMemberRepository : GenericRepository<StuffMembers>, IStuffMemberRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;

        public StuffMemberRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<StuffMembers>> GetAllOrganizationStuffMembers(Guid organizationId)
        {
            return await _Context.StuffMembers.Where(f => f.OrganizationSetupId == organizationId)
                                              .ToListAsync();
        }

        public async Task<StuffMembers?> GetStuffMemberByEmailAsync(string email)
        {
            return await _Context.StuffMembers.FirstOrDefaultAsync(s => s.StuffmemberEmail == email);
        }
    }
}
