using SIA.Domain.Entities;
using SIA.Domain.Models;
using SIA.Infrastructure.Interfaces;
using System.Net;
using System.Net.Mail;

namespace SIA.Infrastructure.Repositories
{
    public class EmailRepository() : IEmailRepository
    {
        public async Task<ResponseMessage> SendMailAsync(SmtpVM smtpModel)
        {
            try
            {
                MailMessage mailMessage = new();
                using SmtpClient smtpClient = new();
                mailMessage.From = new MailAddress(smtpModel.Username, smtpModel.DisplayName);
                mailMessage.To.Add(new MailAddress(smtpModel.ToEmail, smtpModel.ToEmailDisplayName));
                NetworkCredential credentials = new(smtpModel.Username, smtpModel.Password);
                mailMessage.Subject = smtpModel.Subject;
                mailMessage.Body = smtpModel.Body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                smtpClient.Host = smtpModel.SmtpHost;
                smtpClient.Port = smtpModel.SmtpPort;
                smtpClient.EnableSsl = smtpModel.SslEnabled;
                smtpClient.Timeout = 15000; //15 seconds
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = credentials;
                await smtpClient.SendMailAsync(mailMessage);
                return new ResponseMessage(true, "Mail sent successfully");
            }
            catch (Exception ex)
            {
                return new ResponseMessage(false, ex.Message);
            }
        }
    }
}
