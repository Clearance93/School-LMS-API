using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationIInterface.IService.School;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolDashboardsController : ControllerBase
    {
        private readonly ISchoolDashboardServiceInterface _School;

        public SchoolDashboardsController(ISchoolDashboardServiceInterface school)
        {
            _School = school;
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
    }
}
