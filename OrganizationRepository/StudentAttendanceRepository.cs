using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class StudentAttendanceRepository : GenericRepository<StudentAttendance>, IStudentAttendanceInterfaceRepository
    {
        private readonly ApplicationDbContext _Context;
        public StudentAttendanceRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<StudentAttendance>> GertAllStudentAttendance(Guid organizationId, Guid teacherId)
        {
            return await _Context.StudentAttendances.Where(s => s.OrganizationId == organizationId &&
                                                                s.TeacherId == teacherId)
                                                    .ToListAsync();
        }
    }
}
