using OrganizationData;
using OrganizationIInterface.IReporitory;
using OrganizationIInterface.IService;

namespace OrganizationCore.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _Context;
        public IUSerRepositoryInterface Users { get; private set; }

        public IOrganizationRepositoryInterface Organization { get; private set; }

        public IAdminRepositoryInterface Admin {get; private set; }

        public ILearnerrepositoryInterface Learner { get; private set; }

        public IStudentRepository Student { get; private set; }

        public IGuestsRepositoryInterface Guests { get; private set; }

        public ITeacherRepository Teacher { get; private set; }

        public IStuffMemberRepositoryInterface StuffMember { get; private set; }

        public UnitOfWork(ApplicationDbContext context,
                          IUSerRepositoryInterface user,
                          IOrganizationRepositoryInterface organization,
                          IAdminRepositoryInterface admin,
                          ILearnerrepositoryInterface learner,
                          ITeacherRepository teacher,
                          IGuestsRepositoryInterface guest,
                          IStuffMemberRepositoryInterface stuffMember,
                          IStudentRepository student)
        {
            _Context = context;
            Users = user ?? throw new ArgumentNullException(nameof(user));
            Organization = organization ?? throw new ArgumentNullException(nameof(organization));
            Admin = admin ?? throw new ArgumentNullException(nameof(organization));
            Student = student ?? throw new ArgumentNullException(nameof(student));
            Learner = learner ?? throw new ArgumentNullException(nameof(learner));
            StuffMember = stuffMember ?? throw new ArgumentNullException(nameof(stuffMember));
            Guests = guest ?? throw new ArgumentNullException(nameof(guest));
            Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
        }

        public void Dispose()
        {
            _Context.Dispose();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _Context.SaveChangesAsync();
        }
    }
}
