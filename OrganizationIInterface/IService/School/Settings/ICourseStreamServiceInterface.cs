using OrganizationDTO.Dto.Settings;

namespace OrganizationIInterface.IService.School.Settings
{
    public interface ICourseStreamServiceInterface
    {
        Task<bool> AddCourseStreamAsync(CourseStreamDto dto);

        Task<IEnumerable<CourseStreamDto>> GetAllCourseStreamAsync(Guid id);

        Task<CourseStreamDto> GetCourseStreamById(Guid id);

        Task<bool> UpdateCourseStream(Guid id, CourseStreamDto dto);

        Task<bool> DeleteCourseStreamAsync(Guid id);
    }
}
