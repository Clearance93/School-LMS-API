using OrganizationData;
using OrganizationIInterface.IReporitory.Assignments;
using OrganizationModels.Model;

namespace OrganizationRepository.Assignments
{
    public class AssignmentSubmissionRepository : GenericRepository<AssignmentSubmission>, IAssignmentSubmissionRepositoryInterface
    {
        public AssignmentSubmissionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
