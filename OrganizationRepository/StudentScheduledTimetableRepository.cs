using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrganizationDTO.Dto;
using OrganizationIInterface.IReporitory;

namespace OrganizationRepository
{
    public class StudentScheduledTimetableRepository : IStudentScheduledTimetableRepositoryInterface
    {
        private readonly IConfiguration _Configuration;

        public StudentScheduledTimetableRepository(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public async Task<IEnumerable<StudentScheduledTimetableDto>> GetStudentTimeTableAsync(Guid studentId)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var query = @"SELECT * FROM StudentsScheduledTimeTable
                          WHERE StudentId = @studId";

            var dapperRequest = await connection.QueryAsync<StudentScheduledTimetableDto>(query, new { studId = studentId });

            return dapperRequest.ToList();
        }
    }
}
