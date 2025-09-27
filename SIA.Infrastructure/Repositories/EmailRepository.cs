using Microsoft.EntityFrameworkCore;
using SIA.Domain.Entities;
using SIA.Domain.Models;
using SIA.Infrastructure.Data;
using SIA.Infrastructure.DTO;
using SIA.Infrastructure.Interfaces;
using System.Net;
using System.Net.Mail;

namespace SIA.Infrastructure.Repositories
{
    public class EmailRepository(AppDBContext dbContext) : IEmailRepository
    {
        private async Task<SmtpVM> GetSMTPServerAsync(string messageId, string toEmail, string toEmailDisplayName, string bodyParam1, string bodyParam2)
        {
            EmailServer? smtpServer = await dbContext.EmailServers.Where(col => col.IsActive == true).FirstOrDefaultAsync() ?? throw new Exception(AppMessages.MailServerNotConfigured);
            EmailMessage emailMessage = await dbContext.EmailMessages.FirstOrDefaultAsync(col => col.EmailMessageId == messageId) ?? throw new Exception(AppMessages.MailMessageNotConfigured);

            SmtpVM smtpVM = new()
            {
                SmtpHost = smtpServer.EmailSmtpHost,
                SmtpPort = smtpServer.EmailPort,
                Username = smtpServer.EmailUserId,
                Password = smtpServer.EmailPassword,
                SslEnabled = smtpServer.EmailSslenabled,
                DisplayName = emailMessage.EmailDisplayName,
                Subject = string.Format(emailMessage.EmailSubject, toEmailDisplayName, "", ""),
                Body = string.Format(emailMessage.EmailBody, bodyParam1, bodyParam2, ""),
                ToEmail = toEmail,
                ToEmailDisplayName = toEmailDisplayName
            };

            return smtpVM;
        }

        public async Task<ResponseMessage> SendMailAsync(string toEmail, string toEmailDisplayName, string messageId, string bodyParam1 = "", string bodyParam2 = "")
        {
            try
            {
                SmtpVM smtpVM = await GetSMTPServerAsync(messageId, toEmail, toEmailDisplayName, bodyParam1, bodyParam2);
                MailMessage mailMessage = new();
                using SmtpClient smtpClient = new();
                mailMessage.From = new MailAddress(smtpVM.Username, smtpVM.DisplayName);
                mailMessage.To.Add(new MailAddress(smtpVM.ToEmail, smtpVM.ToEmailDisplayName));
                NetworkCredential credentials = new(smtpVM.Username, smtpVM.Password);
                mailMessage.Subject = smtpVM.Subject;
                mailMessage.Body = smtpVM.Body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                smtpClient.Host = smtpVM.SmtpHost;
                smtpClient.Port = smtpVM.SmtpPort;
                smtpClient.EnableSsl = smtpVM.SslEnabled;
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
