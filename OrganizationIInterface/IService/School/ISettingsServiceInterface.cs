using OrganizationDTO.Dto;
using OrganizationDTO.Dto.Settings;

namespace OrganizationIInterface.IService.School
{
    public interface ISettingsServiceInterface
    {
        Task<bool> AddingSchoolGradeWithStreamAsync(GradeWithStreamDto dto);

        Task<IEnumerable<StreamGradeDto>> GetAllStreamByOrganizationId(Guid id);

        Task<IEnumerable<GradeDto>> GetAllGradesByOrganization(Guid organizationId);

        Task<IEnumerable<GradeStreamTeachingClassDto>> GetAllGradeStreamsBasedOnGradeAsync(Guid gradeId);
    }
}
