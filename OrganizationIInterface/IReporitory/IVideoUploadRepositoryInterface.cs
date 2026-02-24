using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface IVideoUploadRepositoryInterface : IGenericRepository<PreRecordedVideo>
    {
        Task<PreRecordedVideo?> GetPreRecordedVideoByStreamId(Guid streamId);

        Task<IEnumerable<PreRecordedVideo>> GetAllTeacherUploadedVideo(Guid teacherId);
    }
}
