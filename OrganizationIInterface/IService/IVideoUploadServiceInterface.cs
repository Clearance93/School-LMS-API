using OrganizationDTO.Dto;

namespace OrganizationIInterface.IService
{
    public interface IVideoUploadServiceInterface
    {
        Task<bool> UploadingVideoAsync(PreRecordedVideoDto dto);

        Task<IEnumerable<PreRecordedVideoDto>> GetAllPreRecordedVideoByStreamidAsync(Guid studentId);

        Task<IEnumerable<PreRecordedVideoDto>> GetAllTeacherUploadedVideosAsync(Guid teacherId);

        Task<PreRecordedVideoDto> GetVideoById(Guid videoId);
    }
}
