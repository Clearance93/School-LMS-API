using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;

namespace OrganizationIInterface.IService
{
    public interface IStudentServiceInterface
    {
        Task<bool> AddNewStudentAsync(CreateStudentDto dto);

        Task<bool> AddNewStudentAttendanceAsync(StudentAttendanceOverViewDto dto);

        Task<bool> AddNewStudentAcademicProgressAsync(AcademicProgressDto dto);

        Task<IEnumerable<StudentAttendanceOverViewDto>> GetAllSTudentAttendanceAsync(Guid studentId);

        Task<IEnumerable<AcademicProgressDto>> GetAllAcademicProgressAsync(Guid studentId);

        Task<bool> UpdateStudentAsync(Guid studentId, UpdateStudentDto dto);

        Task<bool> DeleteStudentAsync(string email);

        Task<StudentDto> GetStudentByEmailAsync(string email);

        Task<IEnumerable<StudentDto>> GetAllStudentsAsync(Guid organizationId);

        Task<bool> AddStudentSubject(StudentGradeDto dto);

        Task<IEnumerable<StudentGradeDto>> GetAllStudentSubjectByStudentIdAsync(Guid studentId);

        Task<bool> DeleteStudentSubjectAsync(Guid studentGradeId);

        Task<IEnumerable<StudentGradeDto>> GetAllOrganizationStudentGrades(Guid organizationId);

        Task<IEnumerable<StudentGradeDto>> GetAllTeacherStudents(Guid teacherId);

        Task<IEnumerable<StudentScheduledTimetableDto>> StudentScheduledTimetableAsync(Guid studentId);
    }
}