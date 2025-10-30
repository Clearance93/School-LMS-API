using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;

namespace OrganizationIInterface.IService
{
    public interface IStudentServiceInterface
    {
        Task<bool> AddNewStudentAsync(CreateStudentDto dto);

        Task<bool> UpdateStudentAsync(Guid studentId, UpdateStudentDto dto);

        Task<bool> DeleteStudentAsync(string email);

        Task<StudentDto> GetStudentByEmailAsync(string email);

        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
    }
}
