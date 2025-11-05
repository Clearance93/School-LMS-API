using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;

namespace OrganizationIInterface.IService
{
    public interface ILearnerServiceInterface
    {
        Task<bool> AddNewLearnerAsync(CreateLearnerDto dto);

        Task<bool> UpdateLearnerAsync(Guid id, UpdateLearnerDto dto);

        Task<bool> DeleteLearnerAsync(string email);

        Task<LearnersDto> GetLearnerByEmailAsync(string email);

        Task<IEnumerable<LearnersDto>> GetAllLearnersAsync();
    }
}
