using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IStudentAttendanceOverviewRepositoryInterface : IGenericRepository<StudentAttendanceOverview>
    {
        Task<IEnumerable<StudentAttendanceOverview>> GetStudentAttendanceByStudentIdAsync(Guid studentId);
    }
}
