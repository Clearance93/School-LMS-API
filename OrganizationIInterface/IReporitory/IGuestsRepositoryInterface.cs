using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IGuestsRepositoryInterface : IGenericRepository<Guests>
    {
        Task<Guests> GetGuestByEmailAsync(string email);
    }
}