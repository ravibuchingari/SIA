
using SIA.Domain.Entities;
using SIA.Domain.Models;

namespace SIA.Infrastructure.Interfaces
{
    public interface IEmailRepository
    {
        Task<ResponseMessage> SendMailAsync(SmtpVM smtpModel);
    }
}
