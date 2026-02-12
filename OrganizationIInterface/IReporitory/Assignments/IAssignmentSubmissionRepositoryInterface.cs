using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory.Assignments
{
    public interface IAssignmentSubmissionRepositoryInterface : IGenericRepository<AssignmentSubmission>
    {
        Task<AssignmentSubmission?> GetTeacherAssignmentsAsync(Guid assignmentId);

        Task<IEnumerable<AssignmentSubmission>> GetAllSubmittedAssignmentByTeacherIdAsync(Guid teacherId);
    }
}
