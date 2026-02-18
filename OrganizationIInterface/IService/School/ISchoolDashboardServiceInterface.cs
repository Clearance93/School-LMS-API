using OrganizationDTO.Dto;
using OrganizationDTO.Dto.Communication;
using OrganizationModels.Model.Communication;

namespace OrganizationIInterface.IService.School
{
    public interface ISchoolDashboardServiceInterface
    {
        Task<SchoolDashboardStatsDto?> GetSchoolDashboardServiceAsync(Guid id);

        Task<IEnumerable<TeacherDashboardViewDto>> GetTeacherDashboardViewAsync(Guid organizationId, Guid teacherId);

        Task<IEnumerable<StudentDashboardDto>> GetStudentDashboardServiceAsync(Guid studentId);

        Task<IEnumerable<MessagesDto>> PullAllMessageSendToReciepentServiceAsync(Guid reciepentId);

        Task<IEnumerable<MessagesDto>> PullAllBrodacastMessageByRoleServiceAsync(string reciepientRole);

        Task<bool> AddNewMessageCommunicationAsync(CreateMessageDto dto);

        Task<bool> AddNewBroadCastMessageAsync(BroadcastMessageDto dto);

        Task<IEnumerable<MessagesDto>> GetAllMessagesByIds(Guid organizationId, Guid senderId);
    }
}
