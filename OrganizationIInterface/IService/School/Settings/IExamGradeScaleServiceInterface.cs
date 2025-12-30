using OrganizationDTO.Dto.Settings;

namespace OrganizationIInterface.IService.School.Settings
{
    public interface IExamGradeScaleServiceInterface
    {
        Task<bool> AddExamGradesScaleAsync(ExamGradeScaleDto dto);

        Task<ExamGradeScaleDto> GetExamGradeScaleByOrganizationId(Guid id);
    }
}
