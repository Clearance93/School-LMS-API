using OrganizationDTO.Dto;

namespace OrganizationIInterface.IService.Assignments
{
    public interface IAssignmentServiceInterface
    {
        Task<bool> CreateAnAssignmentAsync(AssignmentDto dto);

        Task<bool> AddAssignmentGradesAsync(AssignmentGradesDto dto);

        Task<bool> AssignmentsSubmissionAsync(AssignmentSubmissionDto dto);

        Task<IEnumerable<AssignmentDto>> GetAllAssignmentsCreatedByTeacherAsync(Guid teacherId);

        Task<IEnumerable<StudentAssignmentSubmittedDto>> GetAllSubmittedAssignmentsAsync(Guid teacherId);
    }
}
