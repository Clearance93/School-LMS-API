using OrganizationDTO.Dto;
using OrganizationDTO.Dto.AIDto;

namespace OrganizationIInterface.IService.Assignments
{
    public interface IAssignmentServiceInterface
    {
        Task<bool> CreateAnAssignmentAsync(AssignmentDto dto);

        Task<bool> AddAssignmentGradesAsync(AssignmentGradesDto dto);

        Task<AIMarkingResponseDto> ReturnAIResponseAsync(AIMarkingResponseDto dto);

        Task<bool> SavePlagiarismResultsAsync(PlagiarismResultDto dto);

        Task<PlagiarismResultDto?> GetPlagiarismResultsAsync(Guid assignmentId, Guid studentId);

        Task<AIMarkingRequestDto> GettingAllMarkingRequestByAssignmentId(Guid assignmentId, Guid studentId);

        Task<AssignmentSubmissionDto> SubmittedAssignment(Guid assignmentId);

        Task<bool> AssignmentsSubmissionAsync(AssignmentSubmissionDto dto);

        Task<IEnumerable<AssignmentDto>> GetAllAssignmentsCreatedByTeacherAsync(Guid teacherId);

        Task<IEnumerable<StudentAssignmentSubmittedDto>> GetAllSubmittedAssignmentsAsync(Guid teacherId);

        Task<IEnumerable<AssignmentSubmissionDto>> GetAllTeacherAssignedSubmittedAssignmentAsync(Guid teacherId);

        Task<AssignmentSubmissionDto?> GetStudentSubmittedAssignmentByStudentIdAsync(Guid assignmentSubmissionId);
    }
}
