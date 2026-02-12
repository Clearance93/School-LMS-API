using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IStudentSubjectRepositoryInterface : IGenericRepository<StudentsGrade>
    {
        Task<IEnumerable<StudentsGrade>> GetAllTeacherStudentsAsync(Guid teacherId);

        Task<IEnumerable<StudentsGrade>> GetAllOrganizationStudents(Guid organizationId);

        Task<IEnumerable<StudentsGrade>> GetAllStudentsGradeSubjectByStudentIdAsync(Guid studentId);

        Task<StudentsGrade?> GetStudentGradeByStudentId(Guid studentId, Guid streamGradeId);

        Task<IEnumerable<StudentsGrade>> GetAllStudentSubjects(Guid organizationId);

        Task<StudentsGrade?> GetStudentByStudentId(Guid studentId);
    }
}
