using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrganizationDTO.Dto;
using OrganizationIInterface.IReporitory;

namespace OrganizationRepository
{
    public class ClassPerformanceViewRepository : IClassPerformanceViewRepositoryInterface
    {
        private readonly IConfiguration _Configuration;

        public ClassPerformanceViewRepository(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public async Task<IEnumerable<ClassPerformanceDisplayDto>> GetAllClassPerformanceByTeacherAsync(Guid teacherId)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var sql = @"SELECT ClassName,
                               CAST(AveragePerformance AS INT) AS PerformancePercentage
                        FROM ClassPerformanceView
                        WHERE TeacherId = @teacherId
                        ORDER BY ClassName";

            return (await connection.QueryAsync<ClassPerformanceDisplayDto>(
                sql, new
                {
                    TeacherId = teacherId
                })).ToList();
        }

        public async Task<IEnumerable<ClassPerformanceDetailDto>> GetAllClassPerformanceDetailsByTeacherAsync(Guid teacherId)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var sql = @"SELECT * FROM ClassPerformanceView
                        WHERE TeacherId = @teacherId
                        ORDER BY ClassName";

            return (await connection.QueryAsync<ClassPerformanceDetailDto>(
                sql, new
                {
                    TeacherId = teacherId
                })).ToList();
        }

        public async Task<IEnumerable<ClassPerformanceDetailDto>> GetAllClassPerformaneForOrganizationAsync(Guid organizationId)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var sql = @"SELECT * FROM ClassPerformanceView
                        WHERE OrganizationId = @organizationId
                        ORDER BY ClassName";

            return (await connection.QueryAsync<ClassPerformanceDetailDto>(
                sql, new
                {
                    OrganizationId = organizationId
                })).ToList();
        }

        public async Task<ClassPerformanceDetailDto?> GetMyBestPerformingClassesAsync(Guid teacherId)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var sql = @"SELECT TOP 1 * FROM ClassPerformanceView
                        WHERE TeacherId = @teacherId
                            AND TotalGradedSubmissions > 0
                        ORDER BY AveragePerformance DESC";

            return (await connection.QueryFirstOrDefaultAsync<ClassPerformanceDetailDto>(
                sql, new
                {
                    TeacherId = teacherId
                }));
        }

        public async Task<ClassPerformanceDisplayDto?> GetMyClassPerfomanceByIdAsync(Guid streamId, Guid teacherId)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var sql = @"SELECT * FROM ClassPerformanceView
                        WHERE StreamId = @streamId
                            AND TeacherId = @teacherId";

            return (await connection.QueryFirstOrDefaultAsync<ClassPerformanceDisplayDto>(
                sql, new
                {
                    StreamId = streamId,
                    TeacherId = teacherId
                }));
        }

        public async Task<IEnumerable<ClassPerformanceDetailDto>> GetMyLowerPerformingClassesAsync(Guid teacherId, decimal threshold = 70)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var sql = @"SELECT * FROM ClassPerformanceView
                        WHERE TeacherId = @teacherId
                            AND AveragePerformance < @Threshold
                            AND TotalGradedSubmissions > 0
                        ORDER BY AveragePerformance ASC";

            return (await connection.QueryAsync<ClassPerformanceDetailDto>(
                sql, new
                {
                    Threshold = threshold,
                    TeacherId = teacherId
                })).ToList();
        }
    }
}
