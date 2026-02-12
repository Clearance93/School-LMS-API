using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory.Assignments
{
    public interface IAssignmentRepositoryInterface : IGenericRepository<Assignment>
    {
        //Task<IEnumerable<Assignment>> GetAllSubmittedAssignmentByTeacherIdAsync(Guid teacherId);

        Task<IEnumerable<Assignment>> GetAllTeacherAssignmentCreatedAsync(Guid teacherId);

    }
}
