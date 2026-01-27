using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class NewClassRepository : GenericRepository<NewClass>, INewClassRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;

        public NewClassRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<NewClass>> GetAllClassesCreatedByTeacher(Guid organizationId, Guid teacherId)
        {
            return await _Context.NewClass.Where(g => g.OrganizationId == organizationId &&
                                                      g.TeacherId == teacherId)
                                          .ToListAsync();
        }
    }
}
