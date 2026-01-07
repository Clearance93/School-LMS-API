using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class ClassScheduleRepository : GenericRepository<ClassSchedule>, IClassScheduleInterfaceRepository
    {
        private readonly ApplicationDbContext _Context;
        public ClassScheduleRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<ClassSchedule>> GetAllClassScheduledAsync(Guid organizationId, Guid teacherId)
        {
            return await _Context.ClassSchedules.Where(g => g.OrganizationId == organizationId &&
                                                            g.TeacherId == teacherId)
                                                .ToListAsync();
        }
    }
}
