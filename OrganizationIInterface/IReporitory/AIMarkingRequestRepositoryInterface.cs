using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface AIMarkingRequestRepositoryInterface : IGenericRepository<AIMarkingRequest>
    {
        Task<AIMarkingRequest?> ReturningAllMarkingRequest(Guid assignmentId, Guid studentId);
    }
}
