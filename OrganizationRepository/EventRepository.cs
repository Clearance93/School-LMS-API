using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class EventRepository : GenericRepository<Event>, IEventInterfaceRerpository
    {
        private readonly ApplicationDbContext _Context;

        public EventRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync(Guid organizationId)
        {
            return await _Context.Events.Where(g => g.OrganizationId == organizationId)
                                        .ToListAsync();
        }
    }
}
