using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class AdminRepository : GenericRepository<Admins>, IAdminRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;

        public AdminRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Admins?> GetAdminByEmail(string email)
        {
            return await _Context.Admins.FirstOrDefaultAsync(a => a.AdminBusinessEmail == email);
        }
    }
}
