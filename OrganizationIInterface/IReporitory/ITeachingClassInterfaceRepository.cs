using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface ITeachingClassInterfaceRepository : IGenericRepository<TeachingClass>
    {
        Task<IEnumerable<TeachingClass>> GetAllTeachingClassesAsync(Guid organizationId, Guid teacherId);
    }
}
