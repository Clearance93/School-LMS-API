using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IScheduledWorkRpositoryInterface : IGenericRepository<ScheduledWorkshop>
    {
        Task<IEnumerable<ScheduledWorkshop>> GetAllMeetingsByUserAsync(Guid organizationId, Guid userId); 
    }
}
