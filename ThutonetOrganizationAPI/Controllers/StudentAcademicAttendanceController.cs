using Microsoft.AspNetCore.Mvc;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAcademicAttendanceController : ControllerBase
    {
        private readonly IStudentServiceInterface _StudentService;

        public StudentAcademicAttendanceController(IStudentServiceInterface studentService)
        {
            _StudentService = studentService;
        }

        [HttpPost("studentAcedemic")]
        public async Task<IActionResult> AddStudentAcedemicResults(AcademicProgressDto dto)
        {
            try
            {
                var academic = await _StudentService.AddNewStudentAcademicProgressAsync(dto);

                return Ok(academic);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            } 
        }

        [HttpPost("studentAttendance")]
        public async Task<IActionResult> AddStudentAttendance(StudentAttendanceOverViewDto dto)
        {
            try
            {
                var student = await _StudentService.AddNewStudentAttendanceAsync(dto);

                return Ok(student);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpGet("getStudentAcademic/{studentId}")]
        public async Task<IActionResult> GetAllStudentAcademic(Guid studentId)
        {
            try
            {
                var student = await _StudentService.GetAllAcademicProgressAsync(studentId);

                return Ok(student);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpGet("getStudentAttendance/{studentId}")] 
        public async Task<IActionResult> GetAllStudentsAttendance(Guid studentId)
        {
            try
            {
                var student = await _StudentService.GetAllSTudentAttendanceAsync(studentId);

                return Ok(student);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }
    }
}
