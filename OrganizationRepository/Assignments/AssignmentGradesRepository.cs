using OrganizationData;
using OrganizationIInterface.IReporitory.Assignments;
using OrganizationModels.Model;

namespace OrganizationRepository.Assignments
{
    public class AssignmentGradesRepository : GenericRepository<AssignmentGrades>, IAssingmentGradesRepositoryInterface
    {
        public AssignmentGradesRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
