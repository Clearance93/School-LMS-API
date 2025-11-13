using OrganizationDTO.Dto.Settings;

namespace OrganizationIInterface.IService.School
{
    public interface ISettingsServiceInterface
    {
        Task<bool> AddingSchoolGradeWithStreamAsync(GradeWithStreamDto dto);

        Task<IEnumerable<StreamGradeDto>> GetAllStreamByOrganizationId(Guid id); 
    }
}
