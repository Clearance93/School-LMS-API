using OrganizationDTO.Dto;

namespace OrganizationIInterface.IService
{
    public interface IEventServiceInterface
    {
        Task<bool> CreateEventAsync(EventDto dto);

        Task<bool> UpdateEventAsync(Guid eventId, UpdateEventDto dto);

        Task<IEnumerable<EventDto>> GetAllEventsAsync(Guid organizationId);

        Task<EventDto> GetEventByIdAsync(Guid eventId);
    }
}
