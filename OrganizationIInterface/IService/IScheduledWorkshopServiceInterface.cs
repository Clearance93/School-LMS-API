using OrganizationDTO.Dto;

namespace OrganizationIInterface.IService
{
    public interface IScheduledWorkshopServiceInterface
    {
        Task<IEnumerable<ScheduledWorkshopDto>> GetScheduledWorkshopAsync(Guid organizationId, Guid userId);

        Task<bool> CreateAMettingLink(ScheduledWorkshopDto dto);
    }
}
