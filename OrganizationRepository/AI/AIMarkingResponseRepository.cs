using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository.AI
{
    public class AIMarkingResponseRepository : GenericRepository<AIMarkingResponse>, AIMarkingResponseRepositoryInterface
    {
        public AIMarkingResponseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
