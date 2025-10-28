namespace OrganizationCore.Email_Sender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMassage, byte[] attachment = null, string attachmentName = "attachment/pdf");
    }
}
