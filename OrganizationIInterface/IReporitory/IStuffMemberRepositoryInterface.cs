using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IStuffMemberRepositoryInterface : IGenericRepository<StuffMembers>
    {
        Task<StuffMembers?> GetStuffMemberByEmailAsync(string email);
    }
}
