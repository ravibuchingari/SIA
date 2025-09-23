using Microsoft.EntityFrameworkCore;
using SIA.Infrastructure.Data;
using SIA.Infrastructure.DTO;
using SIA.Infrastructure.Interfaces;

namespace SIA.Infrastructure.Repositories
{
    public class UserRepository(AppDBContext dbContext) : IUserRepository
    {
        public async Task<User> UserAuthenticationAsync(string userId, string password)
        {
            User? user = await dbContext.Users.Where(col => col.UserId == userId && col.Password == password).FirstOrDefaultAsync();
            return new User();
        }
    }
}
