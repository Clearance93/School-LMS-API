using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;

namespace OrganizationIInterface.IService
{
    public interface IStuffMemberServiceInterface
    {
        Task<bool> AddNewStuffMemberAsync(CreateStuffMemberDto dto);

        Task<bool> UpdateStuffMemberAsync(Guid id, UpdateStuffMemberDto dto);

        Task<bool> DeleteStuffMemberAsync(string email);

        Task<StuffMemberDto> GetStuffMemberAsync(string email);

        Task<IEnumerable<StuffMemberDto>> GetAllStuffMembersAsync();
    }
}
