using OrganizationDTO.Dto.Settings;

namespace OrganizationIInterface.IService.School
{
    public interface IRegistrationLinkServiceInterface
    {
        Task<string> GetRoleBaseUrlLinkForRegistrationAsync(GeneretingRegistrationLinkDto dto);

        Task<int> GeAllCountPerregLinkAsync(Guid id);
    }
}
