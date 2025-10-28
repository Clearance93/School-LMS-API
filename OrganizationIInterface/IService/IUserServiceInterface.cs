using OrganizationDTO;
using OrganizationDTO.Dto;
using OrganizationModels;

namespace OrganizationIInterface.IService
{
    public interface IUserServiceInterface
    {
        Task<IEnumerable<UserDto>> GetAllActiveUsersAsync();

        Task<UserDto> GetUserByEmailAsync(string email);

        Task<bool> DeactivateUserAsync(string userId);

        Task<ResponseUserDto> CreateUserAsync(CreateUserDto dto);

        Task<UserDto> UpdateUserAsync(string userId, UpdateUserDto dto);

        Task<ResponseUserDto> AuthenticateUserAsync(UserLoginDto dto);

        Task<string> GeneratePasswordResetTOkenRepositoryAsnc(string email);
    }
}
