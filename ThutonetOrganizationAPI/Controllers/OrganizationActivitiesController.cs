using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationDTO;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationActivitiesController : ControllerBase
    {
        private readonly IEventServiceInterface _Events;
        private readonly IActitiesServicesInterface _Activity;

        public OrganizationActivitiesController(IEventServiceInterface events,
                                                IActitiesServicesInterface activity)
        {
            _Events = events ?? throw new ArgumentNullException(nameof(events));
            _Activity = activity ?? throw new ArgumentNullException(nameof(activity));
        }

        [HttpPost("newEvent")]
        public async Task<IActionResult> AddNewEvent(EventDto dto)
        {
            try
            {
                var newEvent = await _Events.CreateEventAsync(dto);

                return Ok(newEvent);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);    
            }
        }

        [HttpGet("allEvents/{organizationId}")]
        public async Task<IActionResult> GetAllEventsByOrganization(Guid organizationId)
        {
            try
            {
                var allEvents = await _Events.GetAllEventsAsync(organizationId);

                return Ok(allEvents);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPut("updateEvent/{eventId}")]
        public async Task<IActionResult> UpdateEvent(Guid eventId, UpdateEventDto dto)
        {
            try
            {
                var updateEvent = await _Events.UpdateEventAsync(eventId, dto);

                return Ok(updateEvent);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("singleEvent/{eventId}")]
        public async Task<IActionResult> GetSingleEventByEventId(Guid eventId)
        {
            try
            {
                var singleEvent = await _Events.GetEventByIdAsync(eventId);

                return Ok(singleEvent);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost("newActivity")]
        public async Task<IActionResult> AddNewActivity(ActivitiesDto dto)
        {
            try
            {
                var activity = await _Activity.CreateNewActivities(dto);

                return Ok(activity);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("updateActivity/{activityId}")]
        public async Task<IActionResult> UpdateActiviy(Guid activityId, UpdateActivitiesDto dto)
        {
            try
            {
                var updateActivity = await _Activity.UpdateNewActivities(activityId, dto);

                return Ok(updateActivity);  
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("allActivities/{organizationId}")]
        public async Task<IActionResult> GetAllActivitiesByOrganization(Guid organizationId)
        {
            try
            {
                var allActivities = await _Activity.GetAllActivitiesByOrganization(organizationId);

                return Ok(allActivities);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }
    }
}
