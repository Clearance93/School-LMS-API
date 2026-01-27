using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadershipProgramController : ControllerBase
    {
        private readonly ILeadershipPropgramServirceInterface _LeadershipProgram;

        public LeadershipProgramController(ILeadershipPropgramServirceInterface leadershipProgram)
        {
            _LeadershipProgram = leadershipProgram ?? throw new ArgumentNullException(nameof(leadershipProgram));
        }

        [HttpGet("allPrograms/{organizationId}/{userId}")]
        public async Task<IActionResult> GetAllLeadershipProgram(Guid organizationId, Guid userId)
        {
            try
            {
                var allPrograms = await _LeadershipProgram.GetAllLeadershipProgramAsync(organizationId, userId);

                return Ok(allPrograms);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost("createProgram")]
        public async Task<IActionResult> CreateNewLeadershipProgram(LeadershipProgramDto dto)
        {
            try
            {
                var program = await _LeadershipProgram.CreateLeadershipProgramAsync(dto);

                return Ok(program);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }
        }
    }
}
