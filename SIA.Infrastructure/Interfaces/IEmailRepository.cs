
using SIA.Domain.Entities;
using SIA.Domain.Models;

namespace SIA.Infrastructure.Interfaces
{
    public interface IEmailRepository
    {
        Task<ResponseMessage> SendMailAsync(string toEmail, string toEmailDisplayName, string messageId, string bodyParam1 = "", string bodyParam2 = "");
    }
}
