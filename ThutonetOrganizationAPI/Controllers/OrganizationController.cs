using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationDTO.Dto.CreateDto;
using OrganizationIInterface.IService;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationServiceInterface _Organization;
        private readonly ILogger<OrganizationController> _Logger;

        public OrganizationController(IOrganizationServiceInterface organization, ILogger<OrganizationController> logger)
        {
            _Organization = organization;
            _Logger = logger;
        }

        [HttpGet("Get-All-Organizations")]
        public async Task<IActionResult> GetAllOrganization()
        {
            try
            {
                var organization = await _Organization.GetAllOrganizationServiceAsync();

                if (organization == null)
                {
                    return NotFound("Organization was not found");
                }

                return Ok(organization);
            }
            catch (OrganizationCore.Exceptions.InvalidOperationException exception)
            {
                _Logger.LogWarning($"No Organization was found {exception}");
            }
            catch (Exception exception)
            {
                _Logger.LogError($"An Error occured during fatching of the organization: {exception.Message}");
            }

            throw new Exception();
        }

        [HttpPost("Add-New-Organization")]
        public async Task<IActionResult> AddNewOrganization(CreateOrganizationDto dto)
        {
            try
            {
                return Ok(await _Organization.AddOrganizationAsync(dto));
            }
            catch (OrganizationCore.Exceptions.InvalidOperationException ex)
            {
                _Logger.LogWarning($"Faild to add new organization: {ex.Message}");
            }
            catch (Exception exception)
            {
                _Logger.LogError($"An error occured while adding {dto.OrganizationName} organization: {exception.Message}");
            }

            throw new Exception();
        }
    }
}
