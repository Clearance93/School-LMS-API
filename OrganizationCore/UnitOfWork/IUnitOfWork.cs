using OrganizationIInterface.IReporitory;
using OrganizationIInterface.IService;

namespace OrganizationCore.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUSerRepositoryInterface Users { get; }

        IOrganizationRepositoryInterface Organization { get; }

        IAdminRepositoryInterface Admin { get; }

        Task<int> SaveChangeAsync();
    }
}
