using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface ILeadershipPropgramRepositoryInterface : IGenericRepository<LeadershipProgram>
    {
        Task<IEnumerable<LeadershipProgram>> GetAllByOrganizationAndUserId(Guid organizationId, Guid userId);
    }
}
