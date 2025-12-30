using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface ILearnerrepositoryInterface : IGenericRepository<Learners>
    {
        Task<Learners?> GetLearnerByEmailAsync(string email);

        Task<IEnumerable<Learners?>> GetAllLearnersByOrganizationId(Guid organizationId);
    }
}
