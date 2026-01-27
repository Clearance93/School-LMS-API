using OrganizationDTO.Dto;

namespace OrganizationIInterface.IService
{
    public interface INewClassServicesInterface
    {
        Task<bool> CreateNewClassAsync(NewClassDto dto);

        Task<IEnumerable<NewClassDto>> GetAllNewCreatedClassesAsync(Guid organizationId, Guid teacherId);

        Task<bool> UpdateClassAsync(Guid id, NewClassDto dto);
    }
}
