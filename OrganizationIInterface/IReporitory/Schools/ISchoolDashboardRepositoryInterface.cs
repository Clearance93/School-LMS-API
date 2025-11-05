using OrganizationDTO.Dto;

namespace OrganizationIInterface.IReporitory.Schools
{
    public interface ISchoolDashboardRepositoryInterface
    {
        Task<SchoolDashboardStatsDto?> GetSchoolDashboardRepositoryAsync(Guid id);
    }
}
