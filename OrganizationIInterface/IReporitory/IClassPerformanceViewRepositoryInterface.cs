using OrganizationDTO.Dto;

namespace OrganizationIInterface.IReporitory
{
    public interface IClassPerformanceViewRepositoryInterface
    {
        Task<IEnumerable<ClassPerformanceDisplayDto>> GetAllClassPerformanceByTeacherAsync(Guid teacherId);

        Task<IEnumerable<ClassPerformanceDetailDto>> GetAllClassPerformanceDetailsByTeacherAsync(Guid teacherId);

        Task<ClassPerformanceDisplayDto?> GetMyClassPerfomanceByIdAsync(Guid streamId, Guid teacherId);

        Task<IEnumerable<ClassPerformanceDetailDto>> GetMyLowerPerformingClassesAsync(Guid teacherId, decimal threshold = 70);

        Task<ClassPerformanceDetailDto?> GetMyBestPerformingClassesAsync(Guid teacherId);

        Task<IEnumerable<ClassPerformanceDetailDto>> GetAllClassPerformaneForOrganizationAsync(Guid organizationId);
    }
}
