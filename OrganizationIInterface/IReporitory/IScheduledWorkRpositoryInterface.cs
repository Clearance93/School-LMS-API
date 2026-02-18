using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IScheduledWorkRpositoryInterface : IGenericRepository<ScheduledWorkshop>
    {
        Task<IEnumerable<ScheduledWorkshop>> GetAllMeetingsByUserAsync(Guid organizationId, Guid userId);

        Task<IEnumerable<object>> GetAllOrganizationRolesAsync();

        Task<ScheduledWorkshop?> GetAllStudentUpcomingClassesAsync(Guid streamGradeId);

        Task<IEnumerable<ScheduledWorkshop>> GetAllMeatingsByRole(string role);
    }
}
