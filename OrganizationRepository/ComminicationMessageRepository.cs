using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model.Communication;

namespace OrganizationRepository
{
    public class ComminicationMessageRepository : GenericRepository<Message>, ICommunicationMessageInterfaceRepository
    {
        private readonly ApplicationDbContext _Context;
        public ComminicationMessageRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Message>> GetAllMessagesByIdsAsync(Guid organizationId, Guid senderId)
        {
            return await _Context.Message.Where(m => m.OrganizationId == organizationId &&
                                                     m.SenderId == senderId)
                                         .ToListAsync();
        }

        public async Task<IEnumerable<Message>> PullAllBrodacastMessageByRoleAsync(string reciepientRole)
        {
            return await _Context.Message.Where(m => m.RecipientRole == reciepientRole && m.IsBrodacast)
                                         .ToListAsync();
        }

        public async Task<IEnumerable<Message>> PullAllMessageSendToReciepentAsync(Guid recipientId)
        {
            return await _Context.Message.Where(m => m.RecipientId == recipientId ||
                                                     m.SenderId == recipientId)
                                         .ToListAsync();
        }
    }
}
