using OrganizationModels.Model;

namespace OrganizationIInterface.IReporitory
{
    public interface INewClassRepositoryInterface : IGenericRepository<NewClass>
    {
        Task<IEnumerable<NewClass>> GetAllClassesCreatedByTeacher(Guid organizationId, Guid teacherId);
    }
}
