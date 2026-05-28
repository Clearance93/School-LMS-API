using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;
using OrganizationIInterface.IService.Assignments;
using System.Reflection.Metadata;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssingmentController : ControllerBase
    {
        private readonly IAssignmentServiceInterface _Assignment;
        private readonly IBlobService _Blob;

        public AssingmentController(IAssignmentServiceInterface assignment, 
                                    IBlobService blob)
        {
            _Assignment = assignment ?? throw new ArgumentNullException(nameof(assignment));
            _Blob = blob ?? throw new ArgumentNullException(nameof(blob));
        }

        [HttpPost("createAssignment")]
        public async Task<IActionResult> CreateAssingment([FromForm]AssignmentDto dto)
        {
            try
            {
                if (dto.AssignmentFormFile != null &&
                    HttpContext.Request.Form.Files.Count > 0)
                {
                    var file = HttpContext.Request.Form.Files[0];

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    var containerName = "assignments";

                    using (var stream = file.OpenReadStream())
                    {
                        var blobDto = await _Blob.UploadFileTOBlobAsync(file, containerName, fileName);

                        dto.AssignmentFile = blobDto.BlobFileUrl;
                    }

                    if (dto.RubricFormFile != null &&
                        HttpContext.Request.Form.Files.Count > 1)
                    {
                        var rubricFile = HttpContext.Request.Form.Files[1];

                        var rubricFileName = Guid.NewGuid().ToString() + Path.GetExtension(rubricFile.FileName);

                        var rubricContainerName = "rubrics";

                        using (var stream = rubricFile.OpenReadStream())
                        {
                            var blobDto = await _Blob.UploadFileTOBlobAsync(rubricFile, rubricContainerName, rubricFileName);
                            dto.TeacherRubricFile = blobDto.BlobFileUrl;
                        }
                    }
                }

                var assignment = await _Assignment.CreateAnAssignmentAsync(dto);

                return Ok(assignment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("submissionAssignment/{assignmentSubmissionId}")]
        public async Task<IActionResult> GetAllStudentSubmissionAssignment(Guid assignmentSubmissionId)
        {
            try
            {
                var subSubmission = await _Assignment.GetStudentSubmittedAssignmentByStudentIdAsync(assignmentSubmissionId);

                return Ok(subSubmission);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getAllTeacherAssignSubm/{teacherId}")]
        public async Task<IActionResult> GettingAllAssignmentSubmittedByStudentUsingTeacherId(Guid teacherId)
        {
            try
            {
                var assignSubm = await _Assignment.GetAllTeacherAssignedSubmittedAssignmentAsync(teacherId);

                return Ok(assignSubm);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getAllTeacherAssignments/{teacherId}")]
        public async Task<IActionResult> TeachersAllAssignments(Guid teacherId)
        {
            try
            {
                var allAssignments = await _Assignment.GetAllSubmittedAssignmentsAsync(teacherId);

                return Ok(allAssignments);
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
