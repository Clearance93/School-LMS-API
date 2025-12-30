using OrganizationDTO.Dto.Settings;

namespace OrganizationIInterface.IService.School.Settings
{
    public interface ILibraryInterfaceService
    {
        Task<bool> AddABookAsync(LibraryItemsDto library);

        Task<IEnumerable<LibraryItemsDto>> GetAllLibraryItemsAsync();

        Task<bool> UpdateABookAsync(Guid id, LibraryItemsDto library);

        Task<LibraryItemsDto> GetABookItemByIdAsync(Guid id);

        Task<bool> DeleteABookItemAsync(Guid id);
    }
}
