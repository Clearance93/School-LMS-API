using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory.Schools.Settings;
using OrganizationModels.Model.Settings;

namespace OrganizationRepository.Settings
{
    public class SchoolSubjectRepository : GenericRepository<SchoolSubjects>, ISchoolSubjectRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;
        public SchoolSubjectRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<SchoolSubjects?> GetSubjectBySubjectNameAsyc(string subjectName)
        {
            return await _Context.SchoolSubjects.FirstOrDefaultAsync(s => s.SubjectName == subjectName);
        }
    }
}
