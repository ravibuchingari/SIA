using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SIA.Domain.Entities;
using SIA.Domain.Models;
using SIA.Infrastructure.Data;
using SIA.Infrastructure.DTO;
using SIA.Infrastructure.Interfaces;

namespace SIA.Infrastructure.Repositories
{
    public class UserRepository(AppDBContext dbContext, IMapper mapper) : BaseRepository(dbContext ?? null), IUserRepository
    {

        public async Task<ResponseMessage> CreateSignUpAccountAsync(UserVM userDTO)
        {
            User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Mail == userDTO.Mail);
            if (user != null)
                return new ResponseMessage(false, AppMessages.DuplicateEmail);

            userDTO.RoleId = 1;
            user = mapper.Map<User>(userDTO);
            await dbContext.Users.AddAsync(user);
            await SaveChangesAsync();
            return new ResponseMessage(true, AppMessages.AccountSuccess);
        }
    }
}
