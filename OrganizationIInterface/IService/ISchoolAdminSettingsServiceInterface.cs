using OrganizationDTO.Dto;

namespace OrganizationIInterface.IService
{
    public interface ISchoolAdminSettingsServiceInterface
    {
        Task<bool> AddGeneralSettingsAsync(SchoolAdminSettingsDto dto);

        Task<SchoolAdminSettingsDto> GetGeneralAdminSchoolSettingsAsyc(Guid id);
    }
}
