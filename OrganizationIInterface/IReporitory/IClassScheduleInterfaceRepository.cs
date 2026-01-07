using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IClassScheduleInterfaceRepository : IGenericRepository<ClassSchedule>
    {
        Task<IEnumerable<ClassSchedule>> GetAllClassScheduledAsync(Guid organizationId, Guid teacherId);
    }
}
