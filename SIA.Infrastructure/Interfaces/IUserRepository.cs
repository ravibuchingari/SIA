using SIA.Domain.Entities;

namespace SIA.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();

    }
}
