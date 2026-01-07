using OrganizationData;
using OrganizationIInterface.IReporitory.Assignments;
using OrganizationModels.Model;

namespace OrganizationRepository.Assignments
{
    public class AssingmentRepository : GenericRepository<Assignment>, IAssignmentRepositoryInterface
    {
        public AssingmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
