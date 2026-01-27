using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class ScheduledWorkshopRepository : GenericRepository<ScheduledWorkshop>, IScheduledWorkRpositoryInterface
    {
        private readonly ApplicationDbContext _Context;
        public ScheduledWorkshopRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<ScheduledWorkshop>> GetAllMeetingsByUserAsync(Guid organizationId, Guid userId)
        {
            var teacher = await _Context.Teachers.FirstOrDefaultAsync(t => t.TeacherId == userId);

            if (teacher != null)
            {
                return await _Context.ScheduledWorkshop.Where(s => s.OrganizationId == organizationId &&
                                                                   s.TeacherId == userId)
                                                       .ToListAsync();
            }
            else
            {
                return await _Context.ScheduledWorkshop.Where(s => s.OrganizationId == organizationId &&
                                                                  s.AdminId == userId)
                                                      .ToListAsync();
            }
        }
    }
}
