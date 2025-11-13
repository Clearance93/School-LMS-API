using Microsoft.AspNetCore.Mvc;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;
using OrganizationIInterface.IService;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServiceInterface _Admin;
        private readonly ILogger<AdminController> _Logger;

        public AdminController(IAdminServiceInterface admin,
                               ILogger<AdminController> logger)
        {
            _Admin = admin ?? throw new ArgumentNullException(nameof(admin));
            _Logger = logger ?? throw new ArgumentNullException((nameof(logger)));
        }

        [HttpGet("getAdminByEmail/{email}")]
        public async Task<IActionResult> GetAdminByEmail(string email)
        {
            try
            {
                var admin = await _Admin.GetAdminByEmail(email);

                return Ok(admin);
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to get the uer based on the email: {email}: {exception.Message}");

                throw;
            }
        }

        [HttpGet("getAdminById/{adminId}")]
        public async Task<IActionResult> GetAdminById(Guid adminId)
        {
            try
            {
                var admin = await _Admin.GetAdminByIdAsync(adminId);

                return Ok(admin);
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to get the uer based on the email: {adminId}: {exception.Message}");

                throw;
            }
        }

        [HttpPut("updateAdmin/{adminId}")]
        public async Task<IActionResult> UpdatingAnAdmin(Guid adminId, UpdateAdminDto dto)
        {
            try
            {
                return Ok(await _Admin.UpdateAdminAsync(adminId, dto));
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to update a user with the id: {adminId}, {exception.Message}");

                throw;
            }
        }
    }
}
