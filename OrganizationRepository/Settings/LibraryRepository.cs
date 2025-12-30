using Microsoft.EntityFrameworkCore;
using OrganizationData;
using OrganizationIInterface.IReporitory.Schools.Settings;
using OrganizationModels.Model.Settings;

namespace OrganizationRepository.Settings
{
    public class LibraryRepository : GenericRepository<LibraryItem>, ILibraryInterfaceRepository
    {
        private readonly ApplicationDbContext _Context;
        public LibraryRepository(ApplicationDbContext context) : base(context)
        {
            _Context = context;
        }

        public Task<LibraryItem?> GetBookByTitleAndAuthorAndYear(string title, string author, string year)
        {
            return _Context.Set<LibraryItem>()
                           .FirstOrDefaultAsync(b => b.Title == title &&
                                                     b.Author == author &&
                                                     b.Year == year);
        }
    }
}
