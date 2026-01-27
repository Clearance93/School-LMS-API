using Microsoft.AspNetCore.Mvc;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsUrlController : ControllerBase
    {
        private readonly IScheduledWorkshopServiceInterface _ScheduledWorkshop;

        public MeetingsUrlController(IScheduledWorkshopServiceInterface scheduledWorkshop)
        {
            _ScheduledWorkshop = scheduledWorkshop ?? throw new ArgumentNullException(nameof(scheduledWorkshop));
        }

        [HttpGet("allMeetings/{organizationId}/{userId}")]
        public async Task<IActionResult> GetAllScheduledMeetings(Guid organizationId, Guid userId)
        {
            try
            {
                var meetings = await _ScheduledWorkshop.GetScheduledWorkshopAsync(organizationId, userId);

                return Ok(meetings);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost("createMeeting")]
        public async Task<IActionResult> CreateNewMeeting(ScheduledWorkshopDto dto)
        {
            try
            {
                var newMeeting = await _ScheduledWorkshop.CreateAMettingLink(dto);

                return Ok(newMeeting);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
