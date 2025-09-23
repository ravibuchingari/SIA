
using SIA.Domain.Entities;
using SIA.Domain.Models;

namespace SIA.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<ResponseMessage> CreateSignUpAccountAsync(UserVM userDTO);
    }
}
