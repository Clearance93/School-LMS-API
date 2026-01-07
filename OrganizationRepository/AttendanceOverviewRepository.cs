using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrganizationIInterface.IReporitory;

namespace OrganizationRepository
{
    public class AttendanceOverviewRepository : IAttendanceOverViewRepositoryInterface
    {
        private readonly IConfiguration _Configuration;

        public AttendanceOverviewRepository(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public async Task RebuildDailyAttendanceOverViewAsync(Guid organizationId, Guid teacherId, DateTime date)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var sql = @"
                -- 1. Remove existing overview for the day
                DELETE FROM AttendanceOverviews
                WHERE OrganizationId = @OrganizationId
                  AND TeacherId = @TeacherId
                  AND Date = @Date;
                
                -- 2. Insert fresh aggregated data
                INSERT INTO AttendanceOverviews (
                    AttendanceOverviewId,
                    OrganizationId,
                    TeacherId,
                    GradeStreamId,
                    TeachingClassId,
                    DailyPresent,
                    DailyAbsent,
                    Date
                )
                SELECT
                    NEWID(),
                    sa.OrganizationId,
                    sa.TeacherId,
                    cs.GradeStreamId,
                    tc.TeachingClassId,
                    SUM(CASE WHEN sa.IsPresent = 1 THEN 1 ELSE 0 END),
                    SUM(CASE WHEN sa.IsPresent = 0 THEN 1 ELSE 0 END),
                    ass.Date
                FROM StudentAttendances sa
                JOIN AttendanceSessions ass ON sa.AttendanceSessionId = ass.AttendanceSessionId
                JOIN ClassSchedules cs ON ass.ClassScheduleId = cs.ClassScheduleId
                JOIN TeachingClass tc ON cs.TeachingClassId = tc.TeachingClassId
                WHERE sa.OrganizationId = @OrganizationId
                  AND sa.TeacherId = @TeacherId
                  AND ass.Date = @Date
                GROUP BY sa.OrganizationId, sa.TeacherId, cs.GradeStreamId, tc.TeachingClassId, ass.Date;
            ";

            await connection.ExecuteAsync(sql, new
            {
                OrganizationId = organizationId,
                TeacherId = teacherId,
                Date = date
            });
        }
    }
}
