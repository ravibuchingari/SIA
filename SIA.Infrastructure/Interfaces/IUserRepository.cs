using SIA.Infrastructure.DTO;

namespace SIA.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> UserAuthenticationAsync(string userId, string password);
    }
}
