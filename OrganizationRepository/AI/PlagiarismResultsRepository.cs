using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository.AI
{
    public class PlagiarismResultsRepository : GenericRepository<PlagiarismResults>, IPlagiarismRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;

        public PlagiarismResultsRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<PlagiarismResults?> GetByAssignmentAndStudentAsync(Guid asignmentId, Guid studentId)
        {
            return await _Context.PlagiarismResults.FirstOrDefaultAsync(p => p.AssignmentId == asignmentId &&
                                                                             p.StudentId == studentId);
        }
    }
}
