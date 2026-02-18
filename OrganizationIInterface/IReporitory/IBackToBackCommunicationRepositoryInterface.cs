using OrganizationModels.Model.Communication;

namespace OrganizationIInterface.IReporitory
{
    public interface IBackToBackCommunicationRepositoryInterface : IGenericRepository<BackToBackCommunication>
    {
        Task<IEnumerable<BackToBackCommunication>> GetAllChatHistoryByMessageIdAsync(Guid messageId);
    }
}
