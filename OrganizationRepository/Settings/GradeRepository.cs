using OrganizationData;
using OrganizationIInterface.IReporitory.Schools;
using OrganizationModels.Model.Settings;

namespace OrganizationRepository.Settings
{
    public class GradeRepository : GenericRepository<Grade>, IGradeRepositoryInterface
    {
        public GradeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
