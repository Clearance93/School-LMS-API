using OrganizationDTO.Dto;

namespace OrganizationIInterface.IReporitory.Schools
{
    public interface IStudentDashboardRepositoryInterface
    {
        Task<IEnumerable<StudentDashboardDto>> GetStudentDashboardAsync(Guid studentId, string Grade);
    }
}
