using OrganizationModels.Model.Settings;

namespace OrganizationIInterface.IReporitory.Schools.Settings
{
    public interface ILibraryInterfaceRepository : IGenericRepository<LibraryItem>
    {
        Task<LibraryItem?> GetBookByTitleAndAuthorAndYear(string title, string author, string year);
    }
}