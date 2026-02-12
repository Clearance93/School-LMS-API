using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrganizationDTO.Dto;
using OrganizationIInterface.IReporitory.Schools;

namespace OrganizationRepository.Schools
{
    public class StudentDashboardRepository : IStudentDashboardRepositoryInterface
    {
        private readonly IConfiguration _Configuration;

        public StudentDashboardRepository(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public async Task<IEnumerable<StudentDashboardDto>> GetStudentDashboardAsync(Guid studentId, string grade)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var query = @"SELECT * FROM vw_StudentDashboard
                          WHERE StudentId = @orgStudentId 
                          AND GradeName = @orgGrade";

            var dapperRequest = await connection.QueryAsync<StudentDashboardDto>(query, new { orgStudentId = studentId,
                                                                                              orgGrade = grade});

            return dapperRequest;
        }
    }
}
