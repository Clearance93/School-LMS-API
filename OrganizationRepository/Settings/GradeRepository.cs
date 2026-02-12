using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory.Schools;
using OrganizationModels.Model.Settings;

namespace OrganizationRepository.Settings
{
    public class GradeRepository : GenericRepository<Grade>, IGradeRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;

        public GradeRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Grade>> GetAllGradesByOrganization(Guid organizationId)
        {
            return await _Context.Grades.Where(g => g.OrganizationId == organizationId)
                                        .ToListAsync();
        }

        public async Task<Grade?> GetGradeByGradeStreamIdAsync(string gradeName)
        {
            return await _Context.Grades.FirstOrDefaultAsync(g => g.GradeName == gradeName);
        }

        public async Task<Grade?> GetOrganizationGrade(string gradeName)
        {
            return await _Context.Grades.FirstOrDefaultAsync(g => g.GradeName == gradeName);
        }
    }
}
