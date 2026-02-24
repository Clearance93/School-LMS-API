using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationRepository
{
    public class VideoUploadRepository : GenericRepository<PreRecordedVideo>, IVideoUploadRepositoryInterface
    {
        private readonly ApplicationDbContext _Context;

        public VideoUploadRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<PreRecordedVideo>> GetAllTeacherUploadedVideo(Guid teacherId)
        {
            return await _Context.PreRecordedVideo.Where(p => p.TeacherId == teacherId)
                                                  .ToListAsync();
        }

        public async Task<PreRecordedVideo?> GetPreRecordedVideoByStreamId(Guid streamId)
        {
            return await _Context.PreRecordedVideo.Where(p => p.GradeStreamId == streamId)
                                                  .FirstOrDefaultAsync();
        }
    }
}
