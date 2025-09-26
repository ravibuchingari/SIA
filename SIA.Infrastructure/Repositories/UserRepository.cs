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
        public async Task<ResponseMessage> CreateSignUpAccountAsync(UserVM userVM)
        {
            User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == userVM.Email);
            if (user != null)
                return new ResponseMessage(false, AppMessages.DuplicateEmail);

            userVM.RoleId = 1;
            userVM.UserStatusId = (byte)SIA.Domain.Models.UserStatus.EmailValidation;
            user = mapper.Map<User>(userVM);
            await dbContext.Users.AddAsync(user);
            await SaveChangesAsync();
            return new ResponseMessage(true, AppMessages.AccountSuccess);
        }

        public async Task<UserVM> CreateSocialMediaAccountAsync(UserVM userVM)
        {
            User? user = await dbContext.Users.Where(col => col.Email == userVM.Email).FirstOrDefaultAsync();
            if (user != null)
            {
                user.ProfileImageUrl = userVM.ProfileImageUrl;
            }
            else
            {
                userVM.HashPassword = Guid.NewGuid().ToString();
                userVM.PasswordSalt = Guid.NewGuid().ToString();
                userVM.RoleId = 1;
                userVM.UserStatusId = (byte)SIA.Domain.Models.UserStatus.EmailValidation;
                user = mapper.Map<User>(userVM);
                await dbContext.Users.AddAsync(user);
                await SaveChangesAsync();
            }
            return mapper.Map<UserVM>(user);
        }

    }
}
