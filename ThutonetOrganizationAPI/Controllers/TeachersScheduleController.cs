using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersScheduleController : ControllerBase
    {
        private readonly ITeacherScheduleInterfaceService _Teacher;

        public TeachersScheduleController(ITeacherScheduleInterfaceService teacher)
        {
            _Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
        }

        [HttpPost("classSchedule")]
        public async Task<IActionResult> CreateClassSchedule(ClassScheduleDto dto)
        {
            try
            {
                var classSchedule = await _Teacher.CreateClassScheduleAsync(dto);

                return Ok(classSchedule);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost("studentAttendance")]
        public async Task<IActionResult> AddStudentAttendance(StudentAttendanceDto dto)
        {
            try
            {
                var attendance = await _Teacher.CreateStudentAttendance(dto);

                return Ok(attendance);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
        }

        [HttpPost("teachingClass")]
        public async Task<IActionResult> AddClassToTeach(TeachingClassDto dto)
        {
            try
            {
                var teaching = await _Teacher.CreateTeachingClass(dto);

                return Ok(teaching);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getAllScheduledClasses/{organizationId}/{teacherId}")]
        public async Task<IActionResult> GetAllSchedulledClasses(Guid organizationId, Guid teacherId)
        {
            try
            {
                var allSchedulledClasses = await _Teacher.GetAllClassSchedulesAsync(organizationId, teacherId);

                return Ok(allSchedulledClasses);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getStudentAttendance/{organizationId}/{teacherId}")]
        public async Task<IActionResult> GetAllStudentAttendance(Guid organizationId, Guid teacherId)
        {
            try
            {
                var getAllStudents = await _Teacher.GetAllStudentAttendancesAsync(organizationId, teacherId);

                return Ok(getAllStudents);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("teacherSubjestGrade/{teacherId}")]
        public async Task<IActionResult> GetAllTeacherSubjectForHisGrades(Guid teacherId)
        {
            try
            {
                var subTeacher = await _Teacher.GetAllTeacherSubjectGradesAsync(teacherId);

                return Ok(subTeacher);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getAllteachingClasses/{organizationId}/{teacherId}")]
        public async Task<IActionResult> GetAllTeachingClasses(Guid organizationId, Guid teacherId)
        {
            try
            {
                var allTeachingClasses = await _Teacher.GetAllTeachingClassAsync(organizationId, teacherId);

                return Ok(allTeachingClasses);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getAllStreams/{teacherId}")]
        public async Task<IActionResult> GetAllTeacherGradeClassStreams(Guid teacherId)
        {
            try
            {
                var grades = await _Teacher.GetGradeStreamByTeacgerIdAsync(teacherId);

                return Ok(grades);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
