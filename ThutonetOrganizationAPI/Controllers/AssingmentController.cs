using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService.Assignments;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssingmentController : ControllerBase
    {
        private readonly IAssignmentServiceInterface _Assignment;

        public AssingmentController(IAssignmentServiceInterface assignment)
        {
            _Assignment = assignment ?? throw new ArgumentNullException(nameof(assignment));
        }

        [HttpPost("createAssignment")]
        public async Task<IActionResult> CreateAssingment(AssignmentDto dto)
        {
            try
            {
                var assignment = await _Assignment.CreateAnAssignmentAsync(dto);

                return Ok(assignment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getTeacherAssignments/{teacherId}")]
        public async Task<IActionResult> GetAllTeachersAssignments(Guid teacherId)
        {
            try
            {
                var assignments = await _Assignment.GetAllAssignmentsCreatedByTeacherAsync(teacherId);

                return Ok(assignments);
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception.Message}", exception);
            }
        }

        [HttpPost("addAssignmentGrades")]
        public async Task<IActionResult> AddAssignmentSubmissionGrades(AssignmentGradesDto dto)
        {
            try
            {
                var assignment = await _Assignment.AddAssignmentGradesAsync(dto);

                return Ok(assignment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost("assignmentSubmission")]
        public async Task<IActionResult> StudentAssignmentSubmission(AssignmentSubmissionDto dto)
        {
            try
            {
                var assignment = await _Assignment.AssignmentsSubmissionAsync(dto);

                return Ok(assignment);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
        }
    }
}
