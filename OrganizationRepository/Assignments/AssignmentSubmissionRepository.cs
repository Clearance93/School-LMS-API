using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory.Assignments;
using OrganizationModels.Model;

namespace OrganizationRepository.Assignments
{
    public class AssignmentSubmissionRepository : GenericRepository<AssignmentSubmission>, IAssignmentSubmissionRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;

        public AssignmentSubmissionRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<AssignmentSubmission>> GetAllSubmittedAssignmentByTeacherIdAsync(Guid teacherId)
        {
            return await _Context.AssignmentSubmissions.Include(a => a.Assignment)
                                                            .ThenInclude(a => a.GradeStream)
                                                        .Include(a => a.Student)
                                                        .Where(a => a.Assignment!.TeacherId == teacherId)
                                                        .ToListAsync();
        }

        public async Task<AssignmentSubmission?> GetTeacherAssignmentsAsync(Guid assignmentId)
        {
            return await _Context.AssignmentSubmissions.FirstOrDefaultAsync(a => a.AssignmentId == assignmentId);
        }
    }
}
