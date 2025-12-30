using OrganizationModels.Model.Settings;

namespace OrganizationIInterface.IReporitory.Schools.Settings
{
    public interface ICourseStreamRepositoryInterface : IGenericRepository<CourseStreams>
    {
        Task<CourseStreams?> GetStreamByNameAsync(string streamName);

        Task<IEnumerable<CourseStreams>> GetAllCourseStreamAsync(Guid id);
    }
}