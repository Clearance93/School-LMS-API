using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory.Assignments;
using OrganizationModels.Model;

namespace OrganizationRepository.Assignments
{
    public class AssignmentGradesRepository : GenericRepository<AssignmentGrades>, IAssingmentGradesRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;

        public AssignmentGradesRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<AssignmentGrades>> GetAllAssignmentByOrganizationIdAsync(Guid organizationId)
        {
            return await _Context.AssignmentGrades.Where(a => a.OrganizationId == organizationId)
                                                  .ToListAsync();
        }

        public async Task<IEnumerable<AssignmentGrades>> GetAllAssignmentByStudentIdAsync(Guid studentId)
        {
            return await _Context.AssignmentGrades.Where(a => a.StudentId == studentId)
                                                  .ToListAsync();
        }

        public async Task<IEnumerable<AssignmentGrades>> GetAllAssignmentsByTeacherIdAsync(Guid teacherId)
        {
            return await _Context.AssignmentGrades.Where(a => a.TeacherId == teacherId)
                                                  .ToListAsync();
        }
    }
}
