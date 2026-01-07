using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;
using OrganizationModels.Model;

namespace OrganizationServices
{
    public class EventServices : IEventServiceInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly ILogger<EventServices> _Logger;
        private readonly IMapper _Mapper;

        public EventServices(IMapper mapper,
                             ILogger<EventServices> logger,
                             IUnitOfWork unit)
        {
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Logger = logger ?? throw new ArgumentNullException(nameof(_Logger));
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        public async Task<bool> CreateEventAsync(EventDto dto)
        {
            try
            {
                var organization = await _Unit.Organization.GetByIdAsync(dto.OrganizationId);

                if (organization == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The organization with the Id {dto.OrganizationId} does not exist!");
                }

                var events = _Mapper.Map<Event>(dto);

                events.EventId = Guid.NewGuid();
                events.CreatedAt = DateTime.Now;
                events.UpdatedAt = DateTime.Now;

                await _Unit.Events.AddAsync(events);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync(Guid organizationId)
        {
            var events = await _Unit.Events.GetAllEventsAsync(organizationId);

            return _Mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task<EventDto> GetEventByIdAsync(Guid eventId)
        {
            var singleEvent = await _Unit.Events.GetByIdAsync(eventId);

            if (singleEvent == null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"There is not event found with the Id of {eventId}");
            }

            return _Mapper.Map<EventDto>(singleEvent);
        }

        public async Task<bool> UpdateEventAsync(Guid eventId, UpdateEventDto dto)
        {
            try
            {
                var singleEvent = await _Unit.Events.GetByIdAsync(eventId);

                if (singleEvent == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"Event not found with id: {eventId}");
                }

                if (!string.IsNullOrEmpty(dto.Title))
                    singleEvent.Title = dto.Title;

                if (!string.IsNullOrEmpty(dto.Description))
                    singleEvent.Description = dto.Description;

                if (dto.StartTime.HasValue)
                    singleEvent.StartTime = dto.StartTime.Value;

                if (dto.EndTime.HasValue)
                    singleEvent.EndTime = dto.EndTime.Value;

                singleEvent.UpdatedAt = DateTime.Now;

                _Unit.Events.Update(singleEvent);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
