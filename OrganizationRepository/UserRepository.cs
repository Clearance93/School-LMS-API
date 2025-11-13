using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels;

namespace OrganizationRepository
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUSerRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {   
            _Context = context;
        }

        public async Task<ApplicationUser?> CheckEmailConfirmationAsync(string userId)
        {
            return await _Context.Users.FirstOrDefaultAsync(s => s.Id == userId);
        }

        public async Task<IEnumerable<ApplicationUser?>> GetAllActiveUsersAsync()
        {
            return await _Context.ApplicationUsers.Where(user => user.IsActive && !user.IsDeleted)
                                                  .ToListAsync();
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _Context.ApplicationUsers.FirstOrDefaultAsync(user => user.Email == email &&
                                                                              !user.IsDeleted);
        }

        public Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            return _Context.ApplicationUsers.FirstOrDefaultAsync(user => user.Id == userId &&
                                                                          !user.IsDeleted); 
        }

        public async Task<string?> GetUserRoles(string role)
        {
            return await _Context.Roles.Where(r => r.Name == role)
                                       .Select(r => r.Name)
                                       .FirstOrDefaultAsync();
        }
    }
}
