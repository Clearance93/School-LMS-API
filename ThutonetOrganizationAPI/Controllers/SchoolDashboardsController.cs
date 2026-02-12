using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationDTO.Dto.Communication;
using OrganizationDTO.Dto.Settings;
using OrganizationIInterface.IService.School;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolDashboardsController : ControllerBase
    {
        private readonly ISchoolDashboardServiceInterface _School;
        private readonly IRegistrationLinkServiceInterface _Register;

        public SchoolDashboardsController(ISchoolDashboardServiceInterface school,
                                          IRegistrationLinkServiceInterface register)
        {
            _School = school ?? throw new ArgumentNullException(nameof(school));
            _Register = register ?? throw new ArgumentNullException(nameof(register));
        }

        [HttpGet("adminDashboard/{organizationId}")]
        public async Task<IActionResult> GetAdminDashboard(Guid organizationId)
        {
            try
            {
                var adminValues = await _School.GetSchoolDashboardServiceAsync(organizationId);

                    return Ok(adminValues);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        [HttpGet("teacherDashboard/{organizationId}/{teacherId}")]
        public async Task<IActionResult> GetAdminDashboard(Guid organizationId, Guid teacherId)
        {
            try
            {
                var teacherDashboard = await _School.GetTeacherDashboardViewAsync(organizationId, teacherId);

                return Ok(teacherDashboard);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        [HttpGet("studentDashboard/{studentId}")]
        public async Task<IActionResult> GetStudentDashboard(Guid studentId)
        {
            try
            {
                var student = await _School.GetStudentDashboardServiceAsync(studentId);

                return Ok(student);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPost("generatingLinks")]
        public async Task<IActionResult> RegistrationLinkGeneration(GeneretingRegistrationLinkDto dto)
        {
            try
            {
                var link = await _Register.GetRoleBaseUrlLinkForRegistrationAsync(dto);

                return Ok(link);
            }
            catch (Exception exception)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException(exception.Message);
            }
        }

        [HttpGet("getCountStudenReg/{id}")]
        public async Task<IActionResult> RegistrationLinkCount(Guid id)
        {
            try
            {
                var usedLink = await _Register.GeAllCountPerregLinkAsync(id);

                return Ok(usedLink);
            }
            catch (Exception exdeption)
            {
                throw new Exception(exdeption.Message);
            }
        }

        [HttpPost("broadcastMessages")]
        public async Task<IActionResult> BroadCastMessages(BroadcastMessageDto dto)
        {
            try
            {
                var broadcast = await _School.AddNewBroadCastMessageAsync(dto);

                return Ok(broadcast);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        [HttpPost("message")]
        public async Task<IActionResult> Messages(CreateMessageDto dto)
        {
            try
            {
                var message = await _School.AddNewMessageCommunicationAsync(dto);

                return Ok(message); 
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        [HttpGet("getMessages/{organizationId}/{senderId}")]
        public async Task<IActionResult> AllMessageByIds(Guid organizationId, Guid senderId)
        {
            try
            {
                var messages = await _School.GetAllMessagesByIds(organizationId, senderId);

                return Ok(messages);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message + " " + exception.StackTrace);
            }
        }
    }
}
