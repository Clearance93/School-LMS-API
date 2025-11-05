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

        public async Task<Teachers?> GetTeacherByEmailAsync(string email)
        {
            return await _Context.Teachers.FirstOrDefaultAsync(t => t.TeacherEmail == email);
        }
    }
}
