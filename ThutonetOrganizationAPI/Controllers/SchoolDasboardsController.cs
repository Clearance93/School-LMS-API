using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationIInterface.IService.School;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolDasboardsController : ControllerBase
    {
        private readonly ISchoolDashboardServiceInterface _School;

        public SchoolDasboardsController(ISchoolDashboardServiceInterface school)
        {
            _School = school;
        }

        [HttpGet("adminDashboard/{organizationId}")]
        public async Task<IActionResult> GetAdminDashboard(Guid organizationId)
        {
            try
            {
                return Ok(await _School.GetSchoolDashboardServiceAsync(organizationId));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
