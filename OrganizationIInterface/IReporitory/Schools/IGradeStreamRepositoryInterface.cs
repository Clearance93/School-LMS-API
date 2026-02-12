using OrganizationDTO.Dto;
using OrganizationModels.Model.Settings;

namespace OrganizationIInterface.IReporitory.Schools
{
    public interface IGradeStreamRepositoryInterface : IGenericRepository<GradeStream>
    {
        Task<IEnumerable<GradeStream>> GetAllGradeStreams(Guid id);

        Task<IEnumerable<GradeStream>> GetAllTeacherGradeStreamsAsync(Guid teacherId);

        Task<IEnumerable<GradeStreamTeachingClassDto?>> GetAllStreamGradesByGradeIdAsync(Guid gradeId);
    }
}
