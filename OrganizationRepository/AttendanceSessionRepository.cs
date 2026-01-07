using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class AttendanceSessionRepository : GenericRepository<AttendanceSession>, IAttendanceSessionInterfaceRepository
    {
        private readonly ApplicationDbContext _Context;
        public AttendanceSessionRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<AttendanceSession>> GetAllAttendanceSessionAsync(Guid organizationId, Guid teacherId)
        {
            return await _Context.AttendanceSessions.Where(g => g.OrganizationId == organizationId &&
                                                                g.TeacherId == teacherId)
                                                    .ToListAsync();
        }
    }
}
