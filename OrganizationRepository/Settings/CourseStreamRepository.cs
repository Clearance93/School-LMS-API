using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory.Schools.Settings;
using OrganizationModels.Model.Settings;

namespace OrganizationRepository.Settings
{
    public class CourseStreamRepository : GenericRepository<CourseStreams>, ICourseStreamRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;
        public CourseStreamRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<CourseStreams>> GetAllCourseStreamAsync(Guid id)
        {
            return await _Context.CourseStreams.Where(c => c.OrganizationId == id)
                                               .ToListAsync();
        }

        public async Task<CourseStreams?> GetStreamByNameAsync(string streamName)
        {
            return await _Context.CourseStreams.FirstOrDefaultAsync(c => c.CourseStreamName == streamName);
        }
    }
}
