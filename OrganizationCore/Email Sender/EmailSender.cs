using OrganizationCore.Email_Sender;
using System.Net;
using System.Net.Mail;

namespace Services.Repository
{
    public class EmailSender : IEmailSender
    {
        private readonly string _SmtpServer;
        private readonly int _SmtpPort;
        private readonly string _FromEmail;
        private readonly string _SmtpUser;
        private readonly string _SmtpPass;

        public EmailSender(string smtpServer,
                           string fromEmail,
                           string smtpUser,
                           string smtpPass,
                           int smtpPort)
        {
            _SmtpServer = smtpServer;
            _FromEmail = fromEmail;
            _SmtpUser = smtpUser;
            _SmtpPass = smtpPass;
            _SmtpPort = smtpPort;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMassage, byte[] attachment = null, string attachmentName = "attachment.pdf")
        {
            const int maxRetries = 3;
            const int delayBetweenRetries = 2000;

            using (var mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(_FromEmail);
                mailMessage.Subject = subject;
                mailMessage.Body = htmlMassage;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(email);

                if (attachment != null)
                {
                    var attchmentStream = new Attachment(new MemoryStream(attachment), attachmentName, "application/pdf");
                    mailMessage.Attachments.Add(attchmentStream);
                }

                for (int attempt = 0; attempt < maxRetries; attempt++)
                {
                    try
                    {
                        using (var client = new SmtpClient(_SmtpServer))
                        {
                            client.Port = _SmtpPort;
                            client.Credentials = new NetworkCredential(_SmtpUser, _SmtpPass);
                            client.EnableSsl = true;

                            await client.SendMailAsync(mailMessage);
                            return;
                        }
                    }
                    catch (SmtpException ex)
                    {
                        if (attempt == maxRetries - 1)
                        {
                            throw new InvalidOperationException("Error sending email", ex);
                        }

                        await Task.Delay(delayBetweenRetries);
                    }
                }
            }
        }
    }
}
