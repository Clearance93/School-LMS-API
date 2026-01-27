using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationIInterface.IReporitory;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassPerformanceController : ControllerBase
    {
        private readonly IClassPerformanceViewRepositoryInterface _ClassPerformance;

        public ClassPerformanceController(IClassPerformanceViewRepositoryInterface classPerformance)
        {
            _ClassPerformance = classPerformance ?? throw new ArgumentNullException(nameof(classPerformance));
        }

        [HttpGet("myClasses/{teacherId}")]
        public async Task<IActionResult> GetMyClassPerformance(Guid teacherId)
        {
            try
            {
                var teacher = await _ClassPerformance.GetMyBestPerformingClassesAsync(teacherId);

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("myclassesByStream/{streamId}/{teacherId}")]
        public async Task<IActionResult> GetMyClassDetails(Guid streamId, Guid teacherId)
        {
            try
            {
                var teacher = await _ClassPerformance.GetMyClassPerfomanceByIdAsync(streamId, teacherId);

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("lowestPerformance/{teacherId}")]
        public async Task<IActionResult> GetLowerstPerformingClasses(Guid teacherId)
        {
            try
            {
                var teacher = await _ClassPerformance.GetMyLowerPerformingClassesAsync(teacherId);

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("organizationPerformance/{organizationId}")]
        public async Task<IActionResult> GetAllOrganizationPerformance(Guid organizationId)
        {
            try
            {
                var org = await _ClassPerformance.GetAllClassPerformaneForOrganizationAsync(organizationId);

                return Ok(org);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("GetAllTeacherPerformanceDetails/{teacherId}")]
        public async Task<IActionResult> GetAllTeacherClassPerformanceDetails(Guid teacherId)
        {
            try
            {
                var teacher = await _ClassPerformance.GetAllClassPerformanceDetailsByTeacherAsync(teacherId);

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getAllTeacherPerformance/{teacherId}")]
        public async Task<IActionResult> GetAllTeacherPerformance(Guid teacherId)
        {
            try
            {
                var teacher = await _ClassPerformance.GetAllClassPerformanceByTeacherAsync(teacherId);

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
