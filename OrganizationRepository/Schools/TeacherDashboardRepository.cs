using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrganizationDTO.Dto;
using OrganizationIInterface.IReporitory.Schools;

namespace OrganizationRepository.Schools
{
    public class TeacherDashboardRepository : ITeacherDashboardViewRepositoryInterface
    {
        private readonly IConfiguration _Configuration;

        public TeacherDashboardRepository(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public async Task<TeacherDashboardViewDto?> GetTeacherDashboardAsync(Guid organizationId, Guid teacherId)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var query = @"SELECT * FROM TeacherDashboardView
                          WHERE OrganizationId = @orgId 
                          AND TeacherId = @teachId";

            var depperRequest = await connection.QueryFirstOrDefaultAsync<TeacherDashboardViewDto>(query, new {orgId = organizationId,
                                                                                                               teachId = teacherId });

            return depperRequest;
        }
    }
}
