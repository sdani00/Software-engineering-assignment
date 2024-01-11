using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace ScheduleManager.Notification.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public AuthMessageSenderOptions Options { get; }

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                SmtpClient client = new("smtp.gmail.com", 587);

                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                System.Net.NetworkCredential credentials = new(Options.Email, Options.Password);
                client.EnableSsl = true;
                client.Credentials = credentials;
                client.Timeout = 20000;

                MailMessage msg = new(Options.Email, email, subject, htmlMessage);

                msg.IsBodyHtml = true;
                msg.Body = htmlMessage;
                msg.Subject = subject;
                msg.DeliveryNotificationOptions = DeliveryNotificationOptions.None;

                await client.SendMailAsync(msg);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
