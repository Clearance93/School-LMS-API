using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory.Assignments
{
    public interface IAssingmentGradesRepositoryInterface : IGenericRepository<AssignmentGrades>
    {
        Task<IEnumerable<AssignmentGrades>> GetAllAssignmentByOrganizationIdAsync(Guid organizationId);

        Task<IEnumerable<AssignmentGrades>> GetAllAssignmentsByTeacherIdAsync(Guid teacherId);

        Task<IEnumerable<AssignmentGrades>> GetAllAssignmentByStudentIdAsync(Guid studentId);
    }
}
