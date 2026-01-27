using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class TeachingClassRepository : GenericRepository<TeachingClass>, ITeachingClassInterfaceRepository
    {
        private readonly ApplicationDbContext _Context;
        public TeachingClassRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<TeachingClass>> GetAllTeachingClassesAsync(Guid organizationId, Guid teacherId)
        {
            return await _Context.TeachingClass.Where(t => t.OrganizationId == organizationId &&
                                                           t.TeacherId == teacherId)
                                                .ToListAsync();
        }

        public async Task<TeachingClass?> GetTeachingClassByGradeStreamIdAsync(Guid id)
        {
            return await _Context.TeachingClass.FirstOrDefaultAsync(t => t.GradeStreamId == id);
        }
    }
}
