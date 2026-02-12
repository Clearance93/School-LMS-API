using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IAcademicProgressRepositoryInterface : IGenericRepository<AcademicProgress>
    {
        Task<IEnumerable<AcademicProgress>> GetStudentAcademicPerfomanceByStudentIdAsync(Guid studentId);

        Task<AcademicProgress?> GetAcademicResuktsByStudentIdAsync(Guid studentId);
    }
}
