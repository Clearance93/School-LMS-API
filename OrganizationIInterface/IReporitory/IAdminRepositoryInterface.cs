using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IAdminRepositoryInterface : IGenericRepository<Admins>
    {
        Task<Admins?> GetAdminByEmail(string email);
    }
}
