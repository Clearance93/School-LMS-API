using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory.Schools.Settings;
using OrganizationModels.Model.Settings;

namespace OrganizationRepository.Settings
{
    public class ExamGradeScaleRepository : GenericRepository<ExamGradeScale>, IExamGradeScaleRepositoryInterface
    {
        private readonly ApplicationDbContext _Context; 
        public ExamGradeScaleRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public Task<ExamGradeScale?> GetAllExamsGradesByOrganizationIdAsync(Guid id)
        {
            return _Context.ExamGradeScale.FirstOrDefaultAsync(x => x.OrganizationId == id);
        }
    }
}
