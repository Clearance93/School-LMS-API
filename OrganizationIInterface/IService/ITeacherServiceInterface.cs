using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;

namespace OrganizationIInterface.IService
{
    public interface ITeacherServiceInterface
    {
        Task<bool> AddNewTeacherAsync(CreateTeacherDto dto);

        Task<bool> UpdateTeacherAsync(Guid id, UpdateTeacherDto dto);

        Task<bool> DeleteTeacherAsync(string email);

        Task<TeachersDto> GetTeacherByEmailAsync(string email);

        Task<IEnumerable<TeachersDto>> GetAllTeachersAsync(Guid id);
    }
}
