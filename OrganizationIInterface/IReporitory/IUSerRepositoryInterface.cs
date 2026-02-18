using OrganizationModels;

namespace OrganizationIInterface.IReporitory
{
    public interface IUSerRepositoryInterface : IGenericRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetUserByEmailAsync(string email);

        Task<ApplicationUser?> GetUserByIdAsync(string userId);

        Task<IEnumerable<ApplicationUser?>> GetAllActiveUsersAsync();

        Task<string?> GetUserRoles(string role);

        Task<string?> GetUserRoleByUserIdAsync(string email); 

        Task<ApplicationUser?> CheckEmailConfirmationAsync(string userId);
    }
}
