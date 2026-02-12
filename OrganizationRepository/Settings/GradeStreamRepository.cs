using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrganizationData;
using OrganizationDTO.Dto;
using OrganizationIInterface.IReporitory.Schools;
using OrganizationModels.Model.Settings;

namespace OrganizationRepository.Settings
{
    public class GradeStreamRepository : GenericRepository<GradeStream>, IGradeStreamRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;
        private readonly IConfiguration _Configuration;

        public GradeStreamRepository(ApplicationDbContext context,
                                     IConfiguration configuration) : base(context)
        {
            _Context = context;
            _Configuration = configuration;
        }

        public async Task<IEnumerable<GradeStream>> GetAllGradeStreams(Guid id)
        {
            var gradeStreams = await _Context.GradeStreams.Where(g => g.OrganizationId == id)
                                                          .ToListAsync();

            return gradeStreams;
        }

        public async Task<IEnumerable<GradeStreamTeachingClassDto?>> GetAllStreamGradesByGradeIdAsync(Guid gradeId)
        {
            using var connection = new SqlConnection(_Configuration.GetConnectionString("ConnectionString"));

            var query = @"SELECT * FROM vw_GradeStreamTeachingClass
                          WHERE GradeId = @orgGradeId";

            var dapperRequest = await connection.QueryAsync<GradeStreamTeachingClassDto>(query, new { orgGradeId = gradeId});

            return dapperRequest!;
        }

        public async Task<IEnumerable<GradeStream>> GetAllTeacherGradeStreamsAsync(Guid teacherId)
        {
            return await _Context.GradeStreams.Where(g => g.TeacherId == teacherId)
                                              .ToListAsync() ;
        }
    }
}
