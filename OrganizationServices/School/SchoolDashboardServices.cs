
using OrganizationDTO.Dto;
using OrganizationIInterface.IReporitory.Schools;
using OrganizationIInterface.IService.School;

namespace OrganizationServices.School
{
    public class SchoolDashboardServices : ISchoolDashboardServiceInterface
    {
        private readonly ISchoolDashboardRepositoryInterface _School;

        public SchoolDashboardServices(ISchoolDashboardRepositoryInterface school)
        {
            _School = school;
        }

        public async Task<SchoolDashboardStatsDto?> GetSchoolDashboardServiceAsync(Guid id)
        {
            return await _School.GetSchoolDashboardRepositoryAsync(id);
        }
    }
}
