using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IStudentAttendanceInterfaceRepository : IGenericRepository<StudentAttendance>
    {
        Task<IEnumerable<StudentAttendance>> GertAllStudentAttendance(Guid organizationId, Guid teacherId);
    }
}
