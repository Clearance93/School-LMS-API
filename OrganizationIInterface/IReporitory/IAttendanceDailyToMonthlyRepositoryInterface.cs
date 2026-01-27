using OrganizationDTO.Dto;

namespace OrganizationIInterface.IReporitory
{
    public interface IAttendanceDailyToMonthlyRepositoryInterface
    {
        Task<IEnumerable<WeeklyAttendanceDto>> GetWeeklyAttendanceByTeacherAsync(Guid teacherId, int year, int weekNumber);

        Task<IEnumerable<WeeklyAttendanceSummaryDto>> GetCurrentWeekAttendanceByTeacherAsync(Guid teacherId);

        Task<WeeklyAttendanceDto?> GetWeeklyAttendanceByClassAsync(Guid teachingClassId, int year, int weekNumber);

        Task<IEnumerable<WeeklyAttendanceDto>> GetWeeklyAttedanceByOrganizationAsync(Guid organizationId, int year, int weekNumber);

        Task<IEnumerable<WeeklyAttendanceDto>> GetWeeklyAttendanceHistoryAsyc(Guid teachingClassId, int numberOfWeeks = 4);

        Task<IEnumerable<MonthlyAttendanceDto>> GetMonhtlyAttendanceByTeacherAsync(Guid teacherId, int year, int month);

        Task<IEnumerable<MonthlyAttendanceSummaryDto>> GetCurrentMonthlyAttendanceByTeacherAsync(Guid teacherId);

        Task<MonthlyAttendanceDto?> GetMonhtlyAttendanceByClassAsync(Guid teachingClassId, int year, int month);

        Task<TeacherAttendanceDashboardDto> GetTeacherDashboardAsync(Guid teacherId);
    }
}
