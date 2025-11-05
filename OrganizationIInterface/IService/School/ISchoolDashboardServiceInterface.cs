using OrganizationDTO.Dto;

namespace OrganizationIInterface.IService.School
{
    public interface ISchoolDashboardServiceInterface
    {
        Task<SchoolDashboardStatsDto?> GetSchoolDashboardServiceAsync(Guid id);
    }
}
