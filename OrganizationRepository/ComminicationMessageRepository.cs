using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model.Communication;

namespace OrganizationRepository
{
    public class ComminicationMessageRepository : GenericRepository<Message>, ICommunicationMessageInterfaceRepository
    {
        public ComminicationMessageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
