using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;

namespace OrganizationIInterface.IService
{
    public interface IGuestServiceInterface
    {
        Task<bool> AddNewGuestAsync(CreateGuestDto dto);

        Task<bool> UpdateGuestAsync(Guid id, UpdateGuestDto dto);

        Task<bool> DeleteGuestAsync(string email);

        Task<GuestsDto> GetGuestAsync(Guid id);

        Task<IEnumerable<GuestsDto>> GetAllGuestAsync(Guid organizationId);
    }
}
