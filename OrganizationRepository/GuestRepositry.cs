using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class GuestRepositry : GenericRepository<Guests>, IGuestsRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;
        public GuestRepositry(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Guests>> GetAllOrganizationGuest(Guid organizationId)
        {
            return await _Context.Guests.Where(g => g.OrganizationSetupId == organizationId).ToListAsync();
        }

        public async Task<Guests?> GetGuestByEmailAsync(string email)
        {
            return await _Context.Guests.FirstOrDefaultAsync(g => g.GuestEmail == email);
        }
    }
}
