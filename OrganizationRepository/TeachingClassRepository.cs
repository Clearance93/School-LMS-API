using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;
using OrganizationModels.Model.Settings;

namespace OrganizationRepository
{
    public class TeachingClassRepository : GenericRepository<TeachingClass>, ITeachingClassInterfaceRepository
    {
        private readonly ApplicationDbContext _Context;
        public TeachingClassRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<TeachingClass>> GetAllTeachingClassesAsync(Guid organizationId, Guid teacherId)
        {
            return await _Context.TeachingClass.Where(t => t.OrganizationId == organizationId &&
                                                           t.TeacherId == teacherId)
                                                .Include(t => t.GradeStream)
                                                .ToListAsync();
        }

        public async Task<TeachingClass?> GetGradeByGradeStreamIdAsync(string subject)
        {
            return await _Context.TeachingClass.FirstOrDefaultAsync(t => t.Subject == subject);
        }

        public async Task<TeachingClass?> GetProperTeachingDataAsync(Guid teacherId, Guid streamId)
        {
            return await _Context.TeachingClass.FirstOrDefaultAsync(t => t.TeacherId == teacherId &&
                                                                         t.GradeStreamId == streamId);
        }

        public async Task<IEnumerable<TeachingClass>> GetTeacherSubjectByGradeAsync(Guid teacherId)
        {
            var teacherSubject = await _Context.TeachingClass.Include(t => t.GradeStream)
                                                             .Where(t => t.TeacherId == teacherId)
                                                             .ToListAsync();

            return teacherSubject;
        }

        public async Task<TeachingClass?> GetTeachingClassByGradeStreamIdAsync(Guid id)
        {
            return await _Context.TeachingClass.FirstOrDefaultAsync(t => t.GradeStreamId == id);
        }
    }
}
