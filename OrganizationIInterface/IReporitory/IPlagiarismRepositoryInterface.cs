using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IPlagiarismRepositoryInterface : IGenericRepository<PlagiarismResults>
    {
        Task<PlagiarismResults?> GetByAssignmentAndStudentAsync(Guid asignmentId, Guid studentId);
    }
}
