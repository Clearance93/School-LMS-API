using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory.Assignments;
using OrganizationModels.Model;

namespace OrganizationRepository.Assignments
{
    public class AssingmentRepository : GenericRepository<Assignment>, IAssignmentRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;
        public AssingmentRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Assignment>> GetAllTeacherAssignmentCreatedAsync(Guid teacherId)
        {
            return await _Context.Assignments.Where(a => a.TeacherId == teacherId)
                                             .ToListAsync();
        }
    }
}
