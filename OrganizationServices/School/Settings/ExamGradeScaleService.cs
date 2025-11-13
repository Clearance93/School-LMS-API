using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto.Settings;
using OrganizationIInterface.IService.School.Settings;
using OrganizationModels.Model.Settings;

namespace OrganizationServices.School.Settings
{
    public class ExamGradeScaleService : IExamGradeScaleServiceInterface
    {
        private readonly IUnitOfWork _Work;
        private readonly ILogger<CourseStreamService> _Logger;
        private readonly IMapper _Mapper;

        public ExamGradeScaleService(IMapper mapper,
                                     IUnitOfWork work,
                                     ILogger<CourseStreamService> logger)
        {
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Work = work ?? throw new ArgumentNullException(nameof(work));
            _Logger = logger ?? throw new ArgumentNullException(nameof(_Logger));
        }

        public async Task<bool> AddExamGradesScaleAsync(ExamGradeScaleDto dto)
        {
            try
            {
                if (dto == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException("Data provided is empty");
                }

                var organization = await _Work.Organization.GetByIdAsync(dto.OrganizationId);

                if (organization == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException("Organization Id cannot be empty");
                }

                var examScaleByOrganization = await _Work.ExamGrade.GetAllExamsGradesByOrganizationIdAsync(dto.OrganizationId);

                if (examScaleByOrganization == null)
                {
                    var examGradeScale = _Mapper.Map<ExamGradeScale>(dto);

                    examGradeScale.ExamGradeScaleId = Guid.NewGuid();
                    examGradeScale.CreatedAt = DateTime.Now;
                    examGradeScale.UpdatedAt = DateTime.Now;

                    await _Work.ExamGrade.AddAsync(examGradeScale);

                    await _Work.SaveChangeAsync();
                }
                else
                {
                    _Mapper.Map(dto, examScaleByOrganization);

                    examScaleByOrganization!.UpdatedAt = DateTime.Now;

                    _Work.ExamGrade.Update(examScaleByOrganization);

                    await _Work.SaveChangeAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "An error occurred while adding exam grade scale.");
                throw;
            }
        }

        public async Task<ExamGradeScaleDto> GetExamGradeScaleByOrganizationId(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The value of the id: {id} is empty/default");
                }

                var exams = await _Work.ExamGrade.GetAllExamsGradesByOrganizationIdAsync(id);

                if (exams == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"No exam grades found with the organization Id: {id}");
                }

                var examDto = _Mapper.Map<ExamGradeScaleDto>(exams);

                return examDto;
            }
            catch (Exception exepetion)
            {
                _Logger.LogError($"Failed to get the exams by using the organization id of :{id}");
                throw new Exception($"An error occurred while retrieving exam grade scales: {exepetion.Message}");
            }
        }
    }
}
