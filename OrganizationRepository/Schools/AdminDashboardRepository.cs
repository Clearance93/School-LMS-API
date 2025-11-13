using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrganizationDTO.Dto;
using OrganizationIInterface.IReporitory.Schools;

namespace OrganizationRepository.Schools
{
    public class AdminDashboardRepository : ISchoolDashboardRepositoryInterface
    {
        private readonly IConfiguration _Configuration;

        public AdminDashboardRepository(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public async Task<SchoolDashboardStatsDto?> GetSchoolDashboardRepositoryAsync(Guid id)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var query = @"SELECT * FROM SchoolDashboardStats
                          WHERE OrganizationSetupId = @orgId";

            var dapperRequest = await connection.QueryFirstOrDefaultAsync<SchoolDashboardStatsDto>(query, new { orgId = id });

            return dapperRequest;
        }
    }
}
