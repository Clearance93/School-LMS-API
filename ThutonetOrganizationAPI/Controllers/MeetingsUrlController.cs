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

        [HttpGet("getRolesByUser/{email}")]
        public async Task<IActionResult> GetUserRoleAsync(string email)
        {
            try
            {
                var roles = await _ScheduledWorkshop.GetAllRolesAsync(email);

                return Ok(roles);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("studentClasses/{studentId}")]
        public async Task<IActionResult> GetAllStudentUpComingClasses(Guid studentId)
        {
            try
            {
                var upcomingClasses = await _ScheduledWorkshop.GetAllStudentUpomingClassesAsync(studentId);

                return Ok(upcomingClasses);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("upcommingSession/{role}")]
        public async Task<IActionResult> UpcomingWorkshop(string role)
        {
            try
            {
                var workshop = await _ScheduledWorkshop.GetAllUpcomingWorkshopByRoleAsync(role);

                return Ok(workshop);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpGet("chatHitsory/{messageId}")]
        public async Task<IActionResult> GetAllChatHistoryByMessageId(Guid messageId)
        {
            try
            {
                var chatHistory = await _ScheduledWorkshop.GettingAllChatHistoryAsync(messageId);

                return Ok(chatHistory);
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception.Message}", exception);
            }
        }

        //[HttpGet("file/{fileName}")]
        //public async Task<IActionResult> GetFileMedia(string fileName)
        //{
        //    try
        //    {
        //        var (fileBytes, contentType, name) = await _ScheduledWorkshop.GetTheMediaFilesAsync(fileName);

        //        return File(fileBytes, contentType, name);
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception (exception.Message, exception);
        //    }
        //}

        [HttpPost("addToChat")]
        public async Task<IActionResult> AddingChatHistory(BackToBackCommunicationDto dto)
        {
            try
            {
                var chatHistory = await _ScheduledWorkshop.SavingAllChatHostoryAsync(dto);

                return Ok(chatHistory);
            }
            catch (Exception exception)
            {
                throw new Exception (exception.Message, exception);
            }
        }

        [HttpPost("createMeeting/{subject}")]
        public async Task<IActionResult> CreateNewMeeting(ScheduledWorkshopDto dto, string subject)
        {
            try
            {
                var newMeeting = await _ScheduledWorkshop.CreateAMettingLink(dto, subject);

                return Ok(newMeeting);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost("createAdminMeeting")]
        public async Task<IActionResult> CreateAdminNewMeeting(ScheduledWorkshopDto dto)
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
