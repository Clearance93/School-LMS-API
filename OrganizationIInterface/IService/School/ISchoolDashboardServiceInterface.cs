using OrganizationDTO.Dto;
using OrganizationDTO.Dto.Communication;

namespace OrganizationIInterface.IService.School
{
    public interface ISchoolDashboardServiceInterface
    {
        Task<SchoolDashboardStatsDto?> GetSchoolDashboardServiceAsync(Guid id);

        Task<TeacherDashboardViewDto?> GetTeacherDashboardViewAsync(Guid organizationId, Guid teacherId);

        Task<bool> AddNewMessageCommunicationAsync(CreateMessageDto dto);

        Task<bool> AddNewBroadCastMessageAsync(BroadcastMessageDto dto);

        Task<IEnumerable<MessagesDto>> GetAllMessagesByIds(Guid organizationId, Guid senderId);
    }
}
