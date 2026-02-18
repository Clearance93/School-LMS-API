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

        public async Task<IEnumerable<ScheduledWorkshop>> GetAllMeatingsByRole(string role)
        {
            return await _Context.ScheduledWorkshop.Where(s => s.Role == role)
                                                   .ToListAsync();
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

        public async Task<IEnumerable<object>> GetAllOrganizationRolesAsync()
        {
            return await _Context.Roles.ToListAsync();
        }

        public async Task<ScheduledWorkshop?> GetAllStudentUpcomingClassesAsync(Guid streamGradeId)
        {
            return await _Context.ScheduledWorkshop.FirstOrDefaultAsync(s => s.GradeStreamId == streamGradeId);
        }
    }
}
