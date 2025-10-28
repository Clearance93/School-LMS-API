using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;

namespace OrganizationIInterface.IService
{
    public interface IAdminServiceInterface
    {
        Task<AdminDto> GetAdminByIdAsync(Guid id);

        Task<AdminDto> GetAdminByEmail(string email);

        Task<UpdateAdminDto> UpdateAdminAsync(Guid adminId, UpdateAdminDto dto);
    }
}
