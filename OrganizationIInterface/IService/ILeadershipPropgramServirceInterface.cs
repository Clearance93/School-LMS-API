using OrganizationDTO.Dto;

namespace OrganizationIInterface.IService
{
    public interface ILeadershipPropgramServirceInterface
    {
        Task<bool> CreateLeadershipProgramAsync(LeadershipProgramDto dto);

        Task<IEnumerable<LeadershipProgramDto>> GetAllLeadershipProgramAsync(Guid organizationId, Guid userId);
    }
}
