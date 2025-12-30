using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto.Settings;
using OrganizationIInterface.IService.School;
using OrganizationModels.Model.Settings;

namespace OrganizationServices.School
{
    public class SettingsService : ISettingsServiceInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly ILogger<SettingsService> _Logger;
        private readonly IMapper _Mapper;

        public SettingsService(IMapper mapper,
                              ILogger<SettingsService> logger,
                              IUnitOfWork unit)
        {
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Logger = logger;
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        public async Task<bool> AddingSchoolGradeWithStreamAsync(GradeWithStreamDto dto)
        {
            try
            {
                if (dto == null)
                {
                   throw new OrganizationCore.Exceptions.InvalidOperationException("Data provided is empty");
                }

                var teacher = await _Unit.Teacher.GetTeacherByEmailAsync(dto.TeacherEmail!);

                var teacherId = teacher!.TeacherId;
                var organizationId = teacher.OrganizationSetupId;

                if (teacher == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The data is missing TeacherId:{dto.TeacherEmail!}");
                }

                var grade = _Mapper.Map<Grade>(dto);

                grade.GradeId = dto.GradeId;
                grade.OrganizationId = organizationId;
                grade.CreatedAt = DateTime.Now;
                grade.UpdatedAt = DateTime.Now;

                await _Unit.Grade.AddAsync(grade);

                var streamData = _Mapper.Map<GradeStream>(dto);

                streamData.StreamId = Guid.NewGuid();
                streamData.GradeId = grade.GradeId; 
                streamData.TeacherId = teacherId;
                streamData.OrganizationId = organizationId;
                streamData.CreatedAt = DateTime.Now;
                streamData.UpdatedAt = DateTime.Now;

                if (_Unit.GradeStream == null)
                {
                    throw new InvalidOperationException("GradeStream repository is not initialized in UnitOfWork.");
                }

                await _Unit.GradeStream.AddAsync(streamData);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to add new grade or stream: {ex}");
            }
        }

        public async Task<IEnumerable<StreamGradeDto>> GetAllStreamByOrganizationId(Guid id)
        {
            var streams = await _Unit.GradeStream.GetAllGradeStreams(id);

            return _Mapper.Map<IEnumerable<StreamGradeDto>>(streams);
        }
    }
}
