using OrganizationIInterface.IReporitory;
using OrganizationIInterface.IService;

namespace OrganizationCore.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUSerRepositoryInterface Users { get; }

        IOrganizationRepositoryInterface Organization { get; }

        IAdminRepositoryInterface Admin { get; }

        ILearnerrepositoryInterface Learner { get; }

        IStudentRepository Student { get; }

        IGuestsRepositoryInterface Guests { get; }

        ITeacherRepository Teacher { get; }

        IStuffMemberRepositoryInterface StuffMember { get; }

        Task<int> SaveChangeAsync();
    }
}
