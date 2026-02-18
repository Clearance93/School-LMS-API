using OrganizationModels.Model.Communication;

namespace OrganizationIInterface.IReporitory
{
    public interface ICommunicationMessageInterfaceRepository : IGenericRepository<Message>
    {
        Task<IEnumerable<Message>> GetAllMessagesByIdsAsync(Guid organizationId, Guid senderId);

        Task<IEnumerable<Message>> PullAllMessageSendToReciepentAsync(Guid recipientId);

        Task<IEnumerable<Message>> PullAllBrodacastMessageByRoleAsync(string reciepientRole);
    }
}
