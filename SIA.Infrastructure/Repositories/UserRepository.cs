using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SIA.Domain.Entities;
using SIA.Domain.Models;
using SIA.Infrastructure.Data;
using SIA.Infrastructure.DTO;
using SIA.Infrastructure.Interfaces;
using System.Security.Cryptography;

namespace SIA.Infrastructure.Repositories
{
    public class UserRepository(AppDBContext dbContext, IMapper mapper) : BaseRepository(dbContext ?? null), IUserRepository
    {
        private async Task<(int?, string)> CreateOrganizationAsync(OrganizationVM organizationVM)
        {
            Organization? organization = await dbContext.Organizations.Where(col => col.Email == organizationVM.Email).FirstOrDefaultAsync();
            if (organization != null)
                return (null, AppMessages.DuplicateOrganizationEmail);
            organization = mapper.Map<Organization>(organizationVM);
            await dbContext.AddAsync(organization);
            await dbContext.SaveChangesAsync();
            return(organization.OrganizationId, "Success");
        }

        public async Task<ResponseMessage> CreateSignUpAccountAsync(UserVM userVM, OrganizationVM? organizationVM)
        {
            User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == userVM.Email);
            if (user != null)
                return new ResponseMessage(false, AppMessages.DuplicateEmail);
            int? OrgId = null;

            if (userVM.IsOrganization)
            {
                (OrgId, string message) = await CreateOrganizationAsync(organizationVM!);
                if (OrgId == null)
                    return new ResponseMessage(false, message);
            }

            userVM.OrganizationId = OrgId;
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

        public async Task<ResponseMessage> UpdateAccountType(int userId, string securityKey, bool isOrganization, OrganizationVM? organization)
        {
            User? user = await IsValidAdminUserAsync(userId, securityKey);
            if (user == null)
                return new ResponseMessage(false, AppMessages.UnauthorizedAccess);

            if (isOrganization)
            {
                (int? OrgId, string message) = await CreateOrganizationAsync(organization!);
                if (OrgId == null)
                    return new ResponseMessage(false, message);
                user.OrganizationId = OrgId;
            }

            user.IsOrganization = isOrganization;
            user.ModifiedDate = DateTime.UtcNow;
            user.ModifiedUser = userId;
            await SaveChangesAsync();
            return new ResponseMessage(true, "Account updated successfully");
        }

        public async Task<ResponseMessage> ConvertIndividualToBusiness(int userId, string securityKey, OrganizationVM organization)
        {
            User? user = await IsValidAdminUserAsync(userId, securityKey);
            if (user == null)
                return new ResponseMessage(false, AppMessages.UnauthorizedAccess);

            if(user.Organization != null)
                return new ResponseMessage(false, AppMessages.AlreadyConvertedToBusiness);

            (int? OrgId, string message) = await CreateOrganizationAsync(organization!);
            if (OrgId == null)
                return new ResponseMessage(false, message);
            user.OrganizationId = OrgId;
            user.IsOrganization = true;
            user.ModifiedDate = DateTime.UtcNow;
            user.ModifiedUser = userId;
            await SaveChangesAsync();
            return new ResponseMessage(true, AppMessages.ConvertedToBusinessSuccess);
        }


    }
}
