using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class TeacherRepository : GenericRepository<Teachers>, ITeacherRepository
    {
        private readonly ApplicationDbContext _Context;

        public TeacherRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Teachers?>> GetAllOrganizationTeachersAsync(Guid id)
        {
            var teachers =  await _Context.Teachers.Where(t => t.OrganizationSetupId == id)
                                                   .ToListAsync();

            return teachers;
        }

        public async Task<Teachers?> GetTeacherByEmailAsync(string email)
        {
            return await _Context.Teachers.FirstOrDefaultAsync(t => t.TeacherEmail == email);
        }

        public async Task<Teachers?> GetTeacherByOrganization(Guid organizationId, Guid teacherId)
        {
            return await _Context.Teachers.FirstOrDefaultAsync(t => t.OrganizationSetupId == organizationId &&
                                                                    t.TeacherId == teacherId);
        }
    }
}
