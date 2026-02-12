using OrganizationDTO.Dto;

namespace OrganizationIInterface.IReporitory
{
    public interface IStudentScheduledTimetableRepositoryInterface
    {
        Task<IEnumerable<StudentScheduledTimetableDto>> GetStudentTimeTableAsync(Guid studentId);
    }
}
