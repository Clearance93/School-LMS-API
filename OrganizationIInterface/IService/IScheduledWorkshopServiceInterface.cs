using OrganizationDTO.Dto;

namespace OrganizationIInterface.IService
{
    public interface IScheduledWorkshopServiceInterface
    {
        Task<IEnumerable<ScheduledWorkshopDto>> GetScheduledWorkshopAsync(Guid organizationId, Guid userId);

        Task<bool> CreateAMettingLink(ScheduledWorkshopDto dto, string subject = null!);

        Task<IEnumerable<object>> GetAllRolesAsync(string email);

        Task<IEnumerable<ScheduledWorkshopDto>> GetAllStudentUpomingClassesAsync(Guid studentId);

        Task<IEnumerable<ScheduledWorkshopDto>> GetAllUpcomingWorkshopByRoleAsync(string role);

        Task<IEnumerable<BackToBackCommunicationDto>> GettingAllChatHistoryAsync(Guid messageId);

        Task<bool> SavingAllChatHostoryAsync(BackToBackCommunicationDto dto);

        //Task<(byte[] FileBytes, string ContentType, string FileName)> GetTheMediaFilesAsync(string fileName);
    }
}