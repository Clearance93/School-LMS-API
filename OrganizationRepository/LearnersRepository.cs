using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class LearnersRepository : GenericRepository<Learners>, ILearnerrepositoryInterface
    {
        private readonly ApplicationDbContext _Context;

        public LearnersRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Learners?>> GetAllLearnersByOrganizationId(Guid organizationId)
        {
            return await _Context.Learners.Where(l => l.OrganizationSetupId == organizationId).ToListAsync();
        }

        public async Task<Learners?> GetLearnerByEmailAsync(string email)
        {
            return await _Context.Learners.FirstOrDefaultAsync(l => l.LeanerEmail == email);
        }
    }
}
