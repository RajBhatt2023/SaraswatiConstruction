using Microsoft.Extensions.Options;
using SaraswatiConstruction.Domain.Models;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SaraswatiConstruction.Utility.CommunicationService
{
    public class EmailService(IOptions<Smtp> smtp) : IEmailService
    {
        private readonly Smtp? _smtp = smtp.Value;
        public bool SendEmail(string? toEmail, StringBuilder body, string subject)
        {
            try
            {
                if (_smtp != null && _smtp.UserName != null && toEmail != null)
                {
                    // Set up SMTP client.
                    using (SmtpClient client = new SmtpClient(_smtp.HostName, _smtp.Port))
                    {
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(_smtp.UserName, _smtp.Password);

                        // Create email message.
                        using (MailMessage mailMessage = new MailMessage())
                        {
                            mailMessage.From = new MailAddress(_smtp.UserName);
                            mailMessage.To.Add(toEmail);
                            mailMessage.Subject = subject;
                            mailMessage.Body = Convert.ToString(body);
                            mailMessage.IsBodyHtml = true;
                            client.Send(mailMessage);
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
