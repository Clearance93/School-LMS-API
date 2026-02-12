using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationDTO.Dto;
using OrganizationDTO.Dto.Settings;
using OrganizationIInterface.IService;
using OrganizationIInterface.IService.School;
using OrganizationIInterface.IService.School.Settings;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISchoolAdminSettingsServiceInterface _Settings;
        private readonly ISettingsServiceInterface _Setting;
        private readonly ISchoolSubjectServiceInterface _Subject;
        private readonly ICourseStreamServiceInterface _CourseStream;
        private readonly IExamGradeScaleServiceInterface _Exams;

        public SettingsController(ISchoolAdminSettingsServiceInterface settings,
                                  ISettingsServiceInterface setting,
                                  ISchoolSubjectServiceInterface subject,
                                  ICourseStreamServiceInterface courseStream,
                                  IExamGradeScaleServiceInterface exams)
        {
            _Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _Setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _Subject = subject ?? throw new ArgumentNullException(nameof(_Subject));
            _CourseStream = courseStream ?? throw new ArgumentNullException(nameof(courseStream));
            _Exams = exams ?? throw new ArgumentNullException(nameof(exams));
        }

        [HttpPost("adminSchoolSettings")]
        public async Task<IActionResult> AddSchoolAdminGeneralSettings(SchoolAdminSettingsDto dto)
        {
            try
            {
                var settings = await _Settings.AddGeneralSettingsAsync(dto);

                return Ok(settings);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to add the general settings for school adim: {ex}");
            }
        }

        [HttpGet("adminSchoolSettingsById/{id}")]
        public async Task<IActionResult> GetSchoolAdminById(Guid id)
        {
            try
            {
                var data = await _Settings.GetGeneralAdminSchoolSettingsAsyc(id);

                return Ok(data);
            }
            catch (Exception exception)
            {
                throw new Exception($"Failed to retrieve data: {exception.Message}");
            }
        }

        [HttpPost("addingGradesAndStream")]
        public async Task<IActionResult> AddingAcademicStructure(GradeWithStreamDto dto)
        {
            try
            {
                dto.GradeId = Guid.NewGuid();

                var results = await _Setting.AddingSchoolGradeWithStreamAsync(dto);

                return Ok(results);
            }
            catch (Exception exception)
            {
                throw new Exception($"Failed to add grade and streams: {exception.Message}");
            }
        }

        [HttpGet("getAllGrades/{organizationId}")]
        public async Task<IActionResult> GetAllOrganizationalGrades(Guid organizationId)
        {
            try
            {
                var orgGrades = await _Setting.GetAllGradesByOrganization(organizationId);

                return Ok(orgGrades);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getAllStreamGrades/{gradeId}")]
        public async Task<IActionResult> GetAllGradeStreamByGradeId(Guid gradeId)
        {
            try
            {
                var allStreams = await _Setting.GetAllGradeStreamsBasedOnGradeAsync(gradeId);

                return Ok(allStreams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("getAllStreams/{organizationId}")]
        public async Task<IActionResult> GettingAlStreams(Guid organizationId)
        {
            try
            {
                var streams = await _Setting.GetAllStreamByOrganizationId(organizationId);

                return Ok(streams);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        [HttpPost("addSchoolSubject")]
        public async Task<IActionResult> AddingNewSubject(SchoolSubjectDto dto)
        {
            try
            {
                var subject = await _Subject.AddSchoolSubjectAsync(dto);

                return Ok(subject);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        [HttpPost("addCourseStream")]
        public async Task<IActionResult> AddingNewCourseStream(CourseStreamDto dto)
        {
            try
            {
                var course = await _CourseStream.AddCourseStreamAsync(dto);

                return Ok(course);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        [HttpGet("getAllCourse/{id}")]
        public async Task<IActionResult> GettingAllCourseStream(Guid id)
        {
            try
            {
                var courses = await _CourseStream.GetAllCourseStreamAsync(id);

                return Ok(courses);
            }
            catch (InvalidOperationException exception)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"Failed to get the streams: {exception.Message}");
            }
        }

        [HttpPost("examGrades")]
        public async Task<IActionResult> AddExamGrades(ExamGradeScaleDto dto)
        {
            try
            {
                var subject = await _Exams.AddExamGradesScaleAsync(dto);
                return Ok(subject);
            }
            catch (InvalidOperationException exception)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"Failed to retrieve data with the provided subject name: {exception.Message}");
            }
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetCourseStreamById(Guid id)
        {
            try
            {
                var course = await _CourseStream.GetCourseStreamById(id);

                return Ok(course);
            }
            catch (InvalidOperationException exception)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"Failed to retrieve data with the provided id: {exception.Message}");
            }
        }

        [HttpPut("updateCourseStream/{id}")]
        public async Task<IActionResult> UpdateCourseStream(Guid id, CourseStreamDto dto)
        {
            try
            {
                var updatedCourse = await _CourseStream.UpdateCourseStream(id, dto);

                return Ok(updatedCourse);
            }
            catch (InvalidOperationException exception)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"Failed to update the course stream: {exception.Message}");
            }
        }

        [HttpPut("updateSchoolSubject/{id}")]
        public async Task<IActionResult> UpdateSchoolSubject(Guid id, SchoolSubjectDto dto)
        {
            try
            {
                var updatedSubject = await _Subject.UpdateSubjectAsync(id, dto);

                return Ok(updatedSubject);
            }
            catch (InvalidOperationException exception)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"Failed to update the subject: {exception.Message}");
            }
        }

        [HttpDelete("deleteCourseStream/{id}")]
        public async Task<IActionResult> DeleteCourseStream(Guid id)
        {
            try
            {
                var deletedCourse = await _CourseStream.DeleteCourseStreamAsync(id);

                return Ok(deletedCourse);
            }
            catch (InvalidOperationException exception)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"Failed to delete the course stream: {exception.Message}");
            }
        }

        [HttpGet("getExamGrades/{organizationId}")]
        public async Task<IActionResult> GetExamGradesByOrganizationId(Guid organizationId)
        {
            try
            {
                var grades = await _Exams.GetExamGradeScaleByOrganizationId(organizationId);

                return Ok(grades);
            }
            catch (InvalidOperationException exception)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"Failed to retrieve exam grades: {exception.Message}");
            }
        }
    }
}
