using OrganizationDTO;
using OrganizationDTO.Dto;

namespace OrganizationIInterface.IService
{
    public interface IActitiesServicesInterface
    {
        Task<bool> CreateNewActivities(ActivitiesDto activities);

        Task<bool> UpdateNewActivities(Guid activitityId, UpdateActivitiesDto activities);

        Task<IEnumerable<ActivitiesDto>> GetAllActivitiesByOrganization(Guid organizationId);
    }
}
