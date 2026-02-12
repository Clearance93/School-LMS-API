using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class StudentSubjectRepository : GenericRepository<StudentsGrade>, IStudentSubjectRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;

        public StudentSubjectRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<StudentsGrade>> GetAllOrganizationStudents(Guid organizationId)
        {
            return await _Context.StudentsGrade.Where(s => s.OrganizationId == organizationId)
                                               .ToListAsync();
        }

        public async Task<IEnumerable<StudentsGrade>> GetAllStudentsGradeSubjectByStudentIdAsync(Guid studentId)
        {
            return await _Context.StudentsGrade.Where(s => s.StudentId == studentId)
                                               .ToListAsync();
        }

        public async Task<IEnumerable<StudentsGrade>> GetAllStudentSubjects(Guid organizationId)
        {
            return await _Context.StudentsGrade.Where(s => s.StudentId == organizationId).ToListAsync();
        }

        public async Task<IEnumerable<StudentsGrade>> GetAllTeacherStudentsAsync(Guid teacherId)
        {
            return await _Context.StudentsGrade.Where(s => s.TeacherId == teacherId)
                                               .ToListAsync();
        }

        public async Task<StudentsGrade?> GetStudentByStudentId(Guid studentId)
        {
            return await _Context.StudentsGrade.FirstOrDefaultAsync(s => s.StudentId == studentId);
        }

        public async Task<StudentsGrade?> GetStudentGradeByStudentId(Guid studentId, Guid streamGradeId)
        {
            return await _Context.StudentsGrade.FirstOrDefaultAsync(s => s.StudentId == studentId &&
                                                                         s.StreamGradeId == streamGradeId);
        }
    }
}
