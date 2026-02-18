using OrganizationModels.Model;
using OrganizationModels.Model.Settings;

namespace OrganizationIInterface.IReporitory
{
    public interface ITeachingClassInterfaceRepository : IGenericRepository<TeachingClass>
    {
        Task<IEnumerable<TeachingClass>> GetAllTeachingClassesAsync(Guid organizationId, Guid teacherId);

        Task<TeachingClass?> GetTeachingClassByGradeStreamIdAsync(Guid id);

        Task<TeachingClass?> GetProperTeachingDataAsync(Guid teacherId, Guid streamId);

        Task<IEnumerable<TeachingClass>> GetTeacherSubjectByGradeAsync(Guid teacherId);

    }
}
