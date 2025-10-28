using OrganizationDTO.Dto;

namespace OrganizationCore.Email_Sender
{
    public interface IUserEmailSenderInterface
    {
        Task<bool> StudentEmailSender(EmailSenderDto dto);

        Task<bool> AdminEmailSender(EmailSenderDto dto);

        Task<bool> GuestEmailSender(EmailSenderDto dto);

        Task<bool> TeacherEmailSender(EmailSenderDto dto);

        Task<bool> StuffMemberEmailSender(EmailSenderDto dto);

        Task<bool> LearnerEmailSender(EmailSenderDto dto);

        Task<bool> PasswordResetAsync(EmailSenderDto dto);
    }
}
