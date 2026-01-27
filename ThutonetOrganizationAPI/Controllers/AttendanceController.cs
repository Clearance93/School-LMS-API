using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationIInterface.IReporitory;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceDailyToMonthlyRepositoryInterface _Attendance;

        public AttendanceController(IAttendanceDailyToMonthlyRepositoryInterface attendance)
        {
            _Attendance = attendance;
        }

        [HttpGet("currentMontlyAttendance/{teacherId}")]
        public async Task<IActionResult> GetCurrentMonthlyStudentAttendance(Guid teacherId)
        {
            try
            {
                var att = await _Attendance.GetCurrentMonthlyAttendanceByTeacherAsync(teacherId);

                return Ok(att);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("currentweeklyAttendance/{teacherId}")]
        public async Task<IActionResult> GetCurrentWeeklyAttendanceByTeacher(Guid teacherId)
        {
            try
            {
                var att = await _Attendance.GetCurrentWeekAttendanceByTeacherAsync(teacherId);

                return Ok(att);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("montlyAttendance/{teachingClassId}/{year}/{monthInNumber}")]
        public async Task<IActionResult> GetMonthlyAttendanceByClass(Guid teachingClassId, int year, int monthInNumber)
        {
            try
            {
                var att = await _Attendance.GetMonhtlyAttendanceByClassAsync(teachingClassId, year, monthInNumber);

                return Ok(att);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getMontlyAttendance/{teacherId}/{year}/{month}")]
        public async Task<IActionResult> GetMonthlyAttendanceByTeacher(Guid teacherId, int year, int month)
        {
            try
            {
                var att = await _Attendance.GetMonhtlyAttendanceByTeacherAsync(teacherId, year, month);

                return Ok(att);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("teacheDashboard/{teacherId}")]
        public async Task<IActionResult> GetTeachersDashboard(Guid teacherId)
        {
            try
            {
                var att = await _Attendance.GetTeacherDashboardAsync(teacherId);

                return Ok(att);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("weklyAttendance/{teacherId}/{year}/{weekNumber}")]
        public async Task<IActionResult> WeklyAttendance(Guid teacherId, int year, int weekNumber)
        {
            try
            {
                var att = await _Attendance.GetWeeklyAttedanceByOrganizationAsync(teacherId, year, weekNumber);

                return Ok(att);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("weklyAttendanceByClass/{teachingClassId}/{year}/{weekNumber}")]
        public async Task<IActionResult> GetWeklyAttendanceByClass(Guid teachingClassId, int year, int weekNumber)
        {
            try
            {
                var att = await _Attendance.GetWeeklyAttendanceByClassAsync(teachingClassId, year, weekNumber);

                return Ok(att);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("weklyAttendanceByTeacher/{teacherId}/{year}/{weekNumber}")]
        public async Task<IActionResult> GetWeklyAttendanceByTeacher(Guid teacherId, int year, int weekNumber)
        {
            try
            {
                var att = await _Attendance.GetWeeklyAttendanceByTeacherAsync(teacherId, year, weekNumber);

                return Ok(att);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("weklyAttendanceHistory/{teacherId}/{year}/{weekNumber}")]
        public async Task<IActionResult> GetWeklyAttendanceHistory(Guid teacherId)
        {
            try
            {
                var att = await _Attendance.GetWeeklyAttendanceHistoryAsyc(teacherId);

                return Ok(att);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
