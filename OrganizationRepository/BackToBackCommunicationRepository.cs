using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model.Communication;

namespace OrganizationRepository
{
    public class BackToBackCommunicationRepository : GenericRepository<BackToBackCommunication>, IBackToBackCommunicationRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;
        public BackToBackCommunicationRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<BackToBackCommunication>> GetAllChatHistoryByMessageIdAsync(Guid messageId)
        {
            return await _Context.BackToBackCommunication.Where(b => b.MessageId == messageId)
                                                         .ToListAsync();
        }

        public async Task<BackToBackCommunication?> GetTheMediaFileByFileNameAsync(string fileName)
        {
            return await _Context.BackToBackCommunication.FirstOrDefaultAsync(b => b.File == fileName);
        }
    }
}
