using OrganizationDTO.Dto;

namespace OrganizationIInterface.IReporitory.Schools
{
    public interface ITeacherDashboardViewRepositoryInterface
    {
        Task<TeacherDashboardViewDto?> GetTeacherDashboardAsync(Guid organizationId, Guid teacherId);
    }
}