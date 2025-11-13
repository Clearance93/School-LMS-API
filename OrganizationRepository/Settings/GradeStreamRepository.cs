using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory.Schools;
using OrganizationModels.Model.Settings;

namespace OrganizationRepository.Settings
{
    public class GradeStreamRepository : GenericRepository<GradeStream>, IGradeStreamRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;
        public GradeStreamRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<GradeStream>> GetAllGradeStreams(Guid id)
        {
            var gradeStreams = await _Context.GradeStreams.Where(g => g.OrganizationId == id)
                                                          .ToListAsync();

            return gradeStreams;
        }
    }
}
