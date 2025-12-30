using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto.Settings;
using OrganizationIInterface.IReporitory.Schools.Settings;
using OrganizationIInterface.IService.School.Settings;
using OrganizationModels.Model.Settings;

namespace OrganizationServices.School.Settings
{
    public class CourseStreamService : ICourseStreamServiceInterface
    {
        private readonly IUnitOfWork _Work;
        private readonly ILogger<CourseStreamService> _Logger;
        private readonly IMapper _Mapper;
        private readonly ICourseStreamRepositoryInterface _CourseStream;

        public CourseStreamService(IMapper mapper,
                                   IUnitOfWork work,
                                   ILogger<CourseStreamService> logger,
                                   ICourseStreamRepositoryInterface courseStream)
        {
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Work = work ?? throw new ArgumentNullException(nameof(work));
            _Logger = logger ?? throw new ArgumentNullException(nameof(_Logger));
            _CourseStream = courseStream ?? throw new ArgumentNullException(nameof(courseStream));
        }

        public async Task<bool> AddCourseStreamAsync(CourseStreamDto dto)
        {
            var stream = await _CourseStream.GetStreamByNameAsync(dto.CourseStreamName!);

            if (stream != null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The course stream {dto.CourseStreamName} with already exist");
            }

            var course = _Mapper.Map<CourseStreams>(dto);

            course.CourseStreamId = Guid.NewGuid();

            await _Work.CourseStream.AddAsync(course);

            await _Work.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteCourseStreamAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The course stream Id cannot be empty");
            }

            var course = await _Work.CourseStream.GetByIdAsync(id);

            if (course == null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"No course stream found with the Id: {id}");
            }

            _Work.CourseStream.Delete(course);

            await _Work.SaveChangeAsync();

            return true;
        }

        public async Task<IEnumerable<CourseStreamDto>> GetAllCourseStreamAsync(Guid id)
        {
            var allCourse = await _CourseStream.GetAllCourseStreamAsync(id);

            return _Mapper.Map<IEnumerable<CourseStreamDto>>(allCourse);
        }

        public async Task<CourseStreamDto> GetCourseStreamById(Guid id)
        {
            var course = await _Work.CourseStream.GetByIdAsync(id);

            return _Mapper.Map<CourseStreamDto>(course);
        }

        public async Task<bool> UpdateCourseStream(Guid id, CourseStreamDto dto)
        {
            if (id == Guid.Empty)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The course stream Id cannot be empty");
            }

            var course = await _Work.CourseStream.GetByIdAsync(id);

            if (course == null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"No course stream found with the Id: {id}");
            }

            if (dto != null)
            {
                dto.CourseStreamId = id;
                dto.OrganizationId = course.OrganizationId;
            }

            _Mapper.Map(dto, course);

            _Work.CourseStream.Update(course!);

            await _Work.SaveChangeAsync();

            return true;
        }
    }
}