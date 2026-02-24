using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoUploadController : ControllerBase
    {
        private readonly IVideoUploadServiceInterface _VideoUpload;

        public VideoUploadController(IVideoUploadServiceInterface videoUpload)
        {
            _VideoUpload = videoUpload ?? throw new ArgumentNullException(nameof(videoUpload));
        }

        [HttpGet("teacherVideos/{teacherId}")]
        public async Task<IActionResult> GetTeacherVideos(Guid teacherId)
        {
            try
            {
                var teacherUpload = await _VideoUpload.GetAllTeacherUploadedVideosAsync(teacherId);

                return Ok(teacherUpload);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost("uploadVideo")]
        public async Task<IActionResult> UploadingVideos(PreRecordedVideoDto dto)
        {
            try
            {
                var upload = await _VideoUpload.UploadingVideoAsync(dto);

                return Ok(upload);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("studentVideos/{studentId}")] 
        public async Task<IActionResult> GetAllStudentPreRecordedVideos(Guid studentId)
        {
            try
            {
                var videos = await _VideoUpload.GetAllPreRecordedVideoByStreamidAsync(studentId);

                return Ok(videos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("video/{videoId}")]
        public async Task<IActionResult> GetTheVideoById(Guid videoId)
        {
            try
            {
                var video = await _VideoUpload.GetVideoById(videoId);

                return Ok(video);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
