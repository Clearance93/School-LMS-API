using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;
using OrganizationModels.Model;

namespace OrganizationServices
{
    public class VideoUploadServices : IVideoUploadServiceInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly IMapper _Mapper;

        public VideoUploadServices(IMapper mapper,
                                   IUnitOfWork unit)
        {
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        public async Task<IEnumerable<PreRecordedVideoDto>> GetAllPreRecordedVideoByStreamidAsync(Guid studentId)
        {
            try
            {
                var studentGrade = new List<PreRecordedVideoDto>();

                var gradeStreams = await _Unit.StudentSubject.GetAllStudentsGradeSubjectByStudentIdAsync(studentId);

                if (gradeStreams == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"There is not student with the Id: {studentId}");
                }

                foreach (var stream in gradeStreams)
                {
                    var stuPreVideos = await _Unit.VideoUpload.GetPreRecordedVideoByStreamId(stream.StreamGradeId);
                    
                    if (stuPreVideos == null)
                    {
                        continue;
                    }
                    
                    var teacher = await _Unit.Teacher.GetByIdAsync(stream.TeacherId);

                     var dto = new PreRecordedVideoDto
                    {
                        VideoTitle = stuPreVideos!.VideoTitle,
                        Description = stuPreVideos.Description,
                        StreamName = stream.StreamName,
                        GradeStreamId = stream.StreamGradeId,
                        VideoUpload = stuPreVideos.VideoUpload,
                        TeacherId = stuPreVideos.TeacherId,
                        TeacherFullNames = $"{teacher!.FirstName} {teacher.LastName}",
                        PreRecordedVideoId = stuPreVideos.PreRecordedVideoId
                    };

                    studentGrade.Add(dto);
                }

                return studentGrade;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<PreRecordedVideoDto>> GetAllTeacherUploadedVideosAsync(Guid teacherId)
        {
            var getteacherUpload = await _Unit.VideoUpload.GetAllTeacherUploadedVideo(teacherId);   

            return _Mapper.Map<IEnumerable<PreRecordedVideoDto>>(getteacherUpload);
        }

        public async Task<PreRecordedVideoDto> GetVideoById(Guid videoId)
        {
            var video = await _Unit.VideoUpload.GetByIdAsync(videoId);

            return _Mapper.Map<PreRecordedVideoDto>(video);
        }

        public async Task<bool> UploadingVideoAsync(PreRecordedVideoDto dto)
        {
            var teacher = await _Unit.Teacher.GetByIdAsync(dto.TeacherId);

            if (teacher == null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The teacher with Id: {dto.TeacherId} does not exist");
            }

            var videoUoload = _Mapper.Map<PreRecordedVideo>(dto);

            videoUoload.PreRecordedVideoId = Guid.NewGuid();
            videoUoload.UploadedTime = DateTime.Now;
            videoUoload.TeacherFullNames = $"{teacher.FirstName} {teacher.LastName}";

            await _Unit.VideoUpload.AddAsync(videoUoload);

            await _Unit.SaveChangeAsync();

            return true;
        }
    }
}
