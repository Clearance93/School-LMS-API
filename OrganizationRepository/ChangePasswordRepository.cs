using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels;

namespace OrganizationRepository
{
    public class ChangePasswordRepository : GenericRepository<ApplicationUser>, IPasswordChangeRepositoryInterface
    {
        public ChangePasswordRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
