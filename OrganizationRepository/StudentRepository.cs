using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class StudentRepository : GenericRepository<Students>, IStudentRepository
    {
        private readonly ApplicationDbContext _Context;

        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<Students?> GetStudentByEmailAsync(string email)
        {
            return await _Context.Students.FirstOrDefaultAsync(s => s.StudentEmail == email);
        }
    }
}
