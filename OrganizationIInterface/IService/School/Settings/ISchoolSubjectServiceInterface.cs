using OrganizationDTO.Dto.Settings;

namespace OrganizationIInterface.IService.School.Settings
{
    public interface ISchoolSubjectServiceInterface
    {
        Task<bool> AddSchoolSubjectAsync(SchoolSubjectDto dto);

        Task<bool> UpdateSubjectAsync(Guid id, SchoolSubjectDto dto);
    }
}
