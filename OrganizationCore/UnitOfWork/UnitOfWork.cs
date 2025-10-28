using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationIInterface.IService;

namespace OrganizationCore.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _Context;
        public IUSerRepositoryInterface Users { get; private set; }

        public IOrganizationRepositoryInterface Organization { get; private set; }

        public IAdminRepositoryInterface Admin {get; private set; }

        public UnitOfWork(ApplicationDbContext context,
                          IUSerRepositoryInterface user,
                          IOrganizationRepositoryInterface organization,
                          IAdminRepositoryInterface admin)
        {
            _Context = context;
            Users = user ?? throw new ArgumentNullException(nameof(user));
            Organization = organization ?? throw new ArgumentNullException(nameof(organization));
            Admin = admin ?? throw new ArgumentNullException(nameof(organization));
        }

        public void Dispose()
        {
            _Context.Dispose();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _Context.SaveChangesAsync();
        }
    }
}
