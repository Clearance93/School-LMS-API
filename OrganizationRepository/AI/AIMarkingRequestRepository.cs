using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository.AI
{
    public class AIMarkingRequestRepository : GenericRepository<AIMarkingRequest>, AIMarkingRequestRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;

        public AIMarkingRequestRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<AIMarkingRequest?> ReturningAllMarkingRequest(Guid assignmentId, Guid studentId)
        {
            return await _Context.AIMarkingRequest.FirstOrDefaultAsync(a => a.AssignmentId == assignmentId &&
                                                                            a.StudentId == studentId);
        }
    }
}
