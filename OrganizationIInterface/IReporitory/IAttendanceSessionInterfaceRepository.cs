using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IAttendanceSessionInterfaceRepository : IGenericRepository<AttendanceSession>
    {
        Task<IEnumerable<AttendanceSession>> GetAllAttendanceSessionAsync(Guid organizationId, Guid teacherId);
    }
}
