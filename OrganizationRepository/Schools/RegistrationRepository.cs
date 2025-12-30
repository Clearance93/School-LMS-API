using OrganizationData;
using OrganizationIInterface.IReporitory.Schools;
using OrganizationModels.Model;

namespace OrganizationRepository.Schools
{
    public class RegistrationRepository : GenericRepository<RegistrrationLink>, IRegistrationLinkRepositoryInterface
    {
        public RegistrationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
