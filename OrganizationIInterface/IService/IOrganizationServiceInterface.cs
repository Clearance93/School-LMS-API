using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;
using OrganizationIInterface.IReporitory;
using OrganizationModels.Model;

namespace OrganizationIInterface.IService
{
    public interface IOrganizationServiceInterface
    {
        Task<bool> AddOrganizationAsync(CreateOrganizationDto dto);

        Task<OrganizationSetupDto> GetOrganizationByIdAsync(Guid Id);

        Task<UpdateOrganizationDto> UpdateOrganizationAsync(Guid id, UpdateOrganizationDto dto);

        Task<IEnumerable<OrganizationSetupDto>> GetAllOrganizationServiceAsync();
    }
}
