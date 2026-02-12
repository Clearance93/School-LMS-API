using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class AcademicProgressRepository : GenericRepository<AcademicProgress>, IAcademicProgressRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;
        public AcademicProgressRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<AcademicProgress?> GetAcademicResuktsByStudentIdAsync(Guid studentId)
        {
            return await _Context.AcademicProgress.FirstOrDefaultAsync(a => a.StudentId == studentId);
        }

        public async Task<IEnumerable<AcademicProgress>> GetStudentAcademicPerfomanceByStudentIdAsync(Guid studentId)
        {
            return await _Context.AcademicProgress.Where(a => a.StudentId == studentId).ToListAsync();
        }
    }
}
