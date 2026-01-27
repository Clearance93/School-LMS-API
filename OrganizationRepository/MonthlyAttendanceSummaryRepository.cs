using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrganizationDTO.Dto;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class MonthlyAttendanceSummaryRepository : IAttendanceDailyToMonthlyRepositoryInterface
    {
        private readonly IConfiguration _Configuration;

        public MonthlyAttendanceSummaryRepository(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public async Task<IEnumerable<MonthlyAttendanceSummaryDto>> GetCurrentMonthlyAttendanceByTeacherAsync(Guid teacherId)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var sql = @"
                SELECT 
                    TeachingClassId,
                    ClassName,
                    Subject,
                    ClassRoomNumber,
                    Month,
                    MonthName,
                    MonthStartDate,
                    MonthEndDate,
                    TotalStudentsForCalculation AS TotalStudents,
                    MonthlyTotalPresent AS TotalPresent,
                    MonthlyTotalAbsent AS TotalAbsent,
                    DaysRecorded,
                    MonthlyAttendancePercentage AS AttendanceRate,
                    StudentCountSource AS DataSource,
                    CASE WHEN StudentCountDiscrepancy IS NOT NULL THEN 1 ELSE 0 END AS HasDiscrepancy
                FROM MonthlyAttendanceView
                WHERE TeacherId = @TeacherId
                  AND Year = DATEPART(YEAR, GETDATE())
                  AND Month = DATEPART(MONTH, GETDATE())
                ORDER BY ClassName, Subject";

            return await connection.QueryAsync<MonthlyAttendanceSummaryDto>(sql, new { teacherId });
        }

        public async Task<IEnumerable<WeeklyAttendanceSummaryDto>> GetCurrentWeekAttendanceByTeacherAsync(Guid teacherId)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var sql = @"
                SELECT 
                    TeachingClassId,
                    ClassName,
                    Subject,
                    ClassRoomNumber,
                    WeekNumber,
                    WeekStartDate,
                    WeekEndDate,
                    TotalStudentsForCalculation AS TotalStudents,
                    WeeklyTotalPresent AS TotalPresent,
                    WeeklyTotalAbsent AS TotalAbsent,
                    DaysRecorded,
                    WeeklyAttendancePercentage AS AttendanceRate,
                    StudentCountSource AS DataSource,
                    CASE WHEN StudentCountDiscrepancy IS NOT NULL THEN 1 ELSE 0 END AS HasDiscrepancy
                FROM WeeklyAttendanceView
                WHERE TeacherId = @TeacherId
                  AND Year = DATEPART(YEAR, GETDATE())
                  AND WeekNumber = DATEPART(WEEK, GETDATE())
                ORDER BY ClassName, Subject";

            return await connection.QueryAsync<WeeklyAttendanceSummaryDto>(sql, new { teacherId });
        }

        public async Task<MonthlyAttendanceDto?> GetMonhtlyAttendanceByClassAsync(Guid teachingClassId, int year, int month)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var sql = @"
                SELECT 
                    OrganizationId,
                    TeacherId,
                    GradeStreamId,
                    TeachingClassId,
                    Subject,
                    ClassRoomNumber,
                    ClassName,
                    Year,
                    Month,
                    MonthName,
                    MonthStartDate,
                    MonthEndDate,
                    ActualStudentsInGrade,
                    ActiveStudentsInGrade,
                    ManualTotalStudents,
                    TotalStudentsForCalculation,
                    StudentCountSource,
                    MonthlyTotalPresent,
                    MonthlyTotalAbsent,
                    DaysRecorded,
                    MonthlyPossibleAttendance,
                    AvgDailyPresent,
                    AvgDailyAbsent,
                    MonthlyAttendancePercentage,
                    MonthlyAttendancePercentageSubmitted,
                    WeeksWithAttendance,
                    StudentCountDiscrepancy,
                    AttendanceDiscrepancy,
                    LastSubmissionDate
                FROM MonthlyAttendanceView
                WHERE TeachingClassId = @TeachingClassId
                  AND Year = @Year
                  AND Month = @Month";

                        return await connection.QueryFirstOrDefaultAsync<MonthlyAttendanceDto>(
                            sql,
                            new { teachingClassId, year, month });
                    }

        public async Task<IEnumerable<MonthlyAttendanceDto>> GetMonhtlyAttendanceByTeacherAsync(Guid teacherId, int year, int month)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));


            var sql = @"
                SELECT 
                    OrganizationId,
                    TeacherId,
                    GradeStreamId,
                    TeachingClassId,
                    Subject,
                    ClassRoomNumber,
                    ClassName,
                    Year,
                    Month,
                    MonthName,
                    MonthStartDate,
                    MonthEndDate,
                    ActualStudentsInGrade,
                    ActiveStudentsInGrade,
                    ManualTotalStudents,
                    TotalStudentsForCalculation,
                    StudentCountSource,
                    MonthlyTotalPresent,
                    MonthlyTotalAbsent,
                    DaysRecorded,
                    MonthlyPossibleAttendance,
                    AvgDailyPresent,
                    AvgDailyAbsent,
                    MonthlyAttendancePercentage,
                    MonthlyAttendancePercentageSubmitted,
                    WeeksWithAttendance,
                    StudentCountDiscrepancy,
                    AttendanceDiscrepancy,
                    LastSubmissionDate
                FROM MonthlyAttendanceView
                WHERE TeacherId = @TeacherId
                  AND Year = @Year
                  AND Month = @Month
                ORDER BY ClassName, Subject";

            return await connection.QueryAsync<MonthlyAttendanceDto>(sql, new { teacherId, year, month });

        }

        public async Task<TeacherAttendanceDashboardDto> GetTeacherDashboardAsync(Guid teacherId)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var sql = @"
                -- Current Week
                SELECT 
                    TeachingClassId,
                    ClassName,
                    Subject,
                    TotalStudentsForCalculation AS TotalStudents,
                    WeeklyTotalPresent AS TotalPresent,
                    WeeklyAttendancePercentage AS AttendanceRate,
                    DaysRecorded,
                    'Week' AS Period
                FROM WeeklyAttendanceView
                WHERE TeacherId = @TeacherId
                  AND Year = DATEPART(YEAR, GETDATE())
                  AND WeekNumber = DATEPART(WEEK, GETDATE());

                -- Current Month
                SELECT 
                    TeachingClassId,
                    ClassName,
                    Subject,
                    TotalStudentsForCalculation AS TotalStudents,
                    MonthlyTotalPresent AS TotalPresent,
                    MonthlyAttendancePercentage AS AttendanceRate,
                    DaysRecorded,
                    'Month' AS Period
                FROM MonthlyAttendanceView
                WHERE TeacherId = @TeacherId
                  AND Year = DATEPART(YEAR, GETDATE())
                  AND Month = DATEPART(MONTH, GETDATE());

                SELECT 
                    TeachingClassId,
                    Subject,
                    SUM(DailyAbsent) AS AbsentToday,
                    SUM(DailyPresent) AS PresentToday,
                    Date
                FROM AttendanceOverviewWithDetailsView
                WHERE TeacherId = @TeacherId
                  AND CAST(Date AS DATE) = CAST(GETDATE() AS DATE)
                GROUP BY TeachingClassId, Subject, Date";

            using (var multi = await connection.QueryMultipleAsync(sql, new { teacherId }))
            {
                var weeklyData = await multi.ReadAsync<dynamic>();
                var monthlyData = await multi.ReadAsync<dynamic>();
                var todayData = await multi.ReadAsync<dynamic>();

                return new TeacherAttendanceDashboardDto
                {
                    WeeklyAttendance = weeklyData.ToList(),
                    MonhtlyAttendance = monthlyData.ToList(),
                    TodayAttendance = todayData.ToList()
                };
            }
        }

        public async Task<IEnumerable<WeeklyAttendanceDto>> GetWeeklyAttedanceByOrganizationAsync(Guid organizationId, int year, int weekNumber)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var sql = @"
                SELECT 
                    OrganizationId,
                    TeacherId,
                    GradeStreamId,
                    TeachingClassId,
                    Subject,
                    ClassRoomNumber,
                    ClassName,
                    Year,
                    WeekNumber,
                    WeekStartDate,
                    WeekEndDate,
                    ActualStudentsInGrade,
                    ActiveStudentsInGrade,
                    ManualTotalStudents,
                    TotalStudentsForCalculation,
                    StudentCountSource,
                    WeeklyTotalPresent,
                    WeeklyTotalAbsent,
                    DaysRecorded,
                    WeeklyPossibleAttendance,
                    AvgDailyPresent,
                    AvgDailyAbsent,
                    WeeklyAttendancePercentage,
                    WeeklyAttendancePercentageSubmitted,
                    StudentCountDiscrepancy,
                    AttendanceDiscrepancy,
                    LastSubmissionDate
                FROM WeeklyAttendanceView
                WHERE OrganizationId = @OrganizationId
                  AND Year = @Year
                  AND WeekNumber = @WeekNumber
                ORDER BY ClassName, Subject";

            return await connection.QueryAsync<WeeklyAttendanceDto>(
                sql,
                new { organizationId, year, weekNumber });
        }

        public async Task<WeeklyAttendanceDto?> GetWeeklyAttendanceByClassAsync(Guid teachingClassId, int year, int weekNumber)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var sql = @"
                SELECT 
                    OrganizationId,
                    TeacherId,
                    GradeStreamId,
                    TeachingClassId,
                    Subject,
                    ClassRoomNumber,
                    ClassName,
                    Year,
                    WeekNumber,
                    WeekStartDate,
                    WeekEndDate,
                    ActualStudentsInGrade,
                    ActiveStudentsInGrade,
                    ManualTotalStudents,
                    TotalStudentsForCalculation,
                    StudentCountSource,
                    WeeklyTotalPresent,
                    WeeklyTotalAbsent,
                    DaysRecorded,
                    WeeklyPossibleAttendance,
                    AvgDailyPresent,
                    AvgDailyAbsent,
                    WeeklyAttendancePercentage,
                    WeeklyAttendancePercentageSubmitted,
                    StudentCountDiscrepancy,
                    AttendanceDiscrepancy,
                    LastSubmissionDate
                FROM WeeklyAttendanceView
                WHERE TeachingClassId = @TeachingClassId
                  AND Year = @Year
                  AND WeekNumber = @WeekNumber";

            return await connection.QueryFirstOrDefaultAsync<WeeklyAttendanceDto>(
                sql,
                new { teachingClassId, year, weekNumber });
        }

        public async Task<IEnumerable<WeeklyAttendanceDto>> GetWeeklyAttendanceByTeacherAsync(Guid teacherId, int year, int weekNumber)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var sql = @"
                SELECT 
                    OrganizationId,
                    TeacherId,
                    GradeStreamId,
                    TeachingClassId,
                    Subject,
                    ClassRoomNumber,
                    ClassName,
                    Year,
                    WeekNumber,
                    WeekStartDate,
                    WeekEndDate,
                    ActualStudentsInGrade,
                    ActiveStudentsInGrade,
                    ManualTotalStudents,
                    TotalStudentsForCalculation,
                    StudentCountSource,
                    WeeklyTotalPresent,
                    WeeklyTotalAbsent,
                    DaysRecorded,
                    WeeklyPossibleAttendance,
                    AvgDailyPresent,
                    AvgDailyAbsent,
                    WeeklyAttendancePercentage,
                    WeeklyAttendancePercentageSubmitted,
                    StudentCountDiscrepancy,
                    AttendanceDiscrepancy,
                    LastSubmissionDate
                FROM WeeklyAttendanceView
                WHERE TeacherId = @TeacherId
                  AND Year = @Year
                  AND WeekNumber = @WeekNumber
                ORDER BY ClassName, Subject";

            return await connection.QueryAsync<WeeklyAttendanceDto>(sql, new { teacherId, year, weekNumber });
        }

        public async Task<IEnumerable<WeeklyAttendanceDto>> GetWeeklyAttendanceHistoryAsyc(Guid teachingClassId, int numberOfWeeks = 4)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var sql = @"
                SELECT TOP (@NumberOfWeeks)
                    OrganizationId,
                    TeacherId,
                    GradeStreamId,
                    TeachingClassId,
                    Subject,
                    ClassRoomNumber,
                    ClassName,
                    Year,
                    WeekNumber,
                    WeekStartDate,
                    WeekEndDate,
                    ActualStudentsInGrade,
                    ActiveStudentsInGrade,
                    ManualTotalStudents,
                    TotalStudentsForCalculation,
                    StudentCountSource,
                    WeeklyTotalPresent,
                    WeeklyTotalAbsent,
                    DaysRecorded,
                    WeeklyPossibleAttendance,
                    AvgDailyPresent,
                    AvgDailyAbsent,
                    WeeklyAttendancePercentage,
                    WeeklyAttendancePercentageSubmitted,
                    StudentCountDiscrepancy,
                    AttendanceDiscrepancy,
                    LastSubmissionDate
                FROM WeeklyAttendanceView
                WHERE TeachingClassId = @TeachingClassId
                ORDER BY Year DESC, WeekNumber DESC";

            return await connection.QueryAsync<WeeklyAttendanceDto>(
                sql,
                new { teachingClassId, numberOfWeeks });
        }
    }
}
