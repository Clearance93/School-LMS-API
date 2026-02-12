using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class StudentAttendanceOverviewRepository : GenericRepository<StudentAttendanceOverview>, IStudentAttendanceOverviewRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;

        public StudentAttendanceOverviewRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<StudentAttendanceOverview>> GetStudentAttendanceByStudentIdAsync(Guid studentId)
        {
            return await _Context.StudentAttendanceOverview.Where(s => s.StudentId == studentId).ToListAsync();
        }
    }
}
