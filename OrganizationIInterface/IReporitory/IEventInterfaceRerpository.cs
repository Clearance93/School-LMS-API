using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IEventInterfaceRerpository : IGenericRepository<Event>
    {
        Task<IEnumerable<Event>> GetAllEventsAsync(Guid organizationId);
    }
}
