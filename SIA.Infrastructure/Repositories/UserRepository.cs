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
    public class UserRepository(AppDBContext dbContext, IEmailRepository emailRepository, IMapper mapper) : BaseRepository(dbContext ?? null), IUserRepository
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

        public async Task<(UserVM?, ResponseMessage)> CreateSignUpAccountAsync(UserVM userVM, OrganizationVM organizationVM)
        {
            User? user = await dbContext.Users.Include(org => org.Organization).FirstOrDefaultAsync(u => u.Email == userVM.Email);
            if (user != null)
                return (null, new ResponseMessage(false, AppMessages.DuplicateEmail));

            (int? OrgId, string message) = await CreateOrganizationAsync(organizationVM!);
            if (OrgId == null)
                return (null, new ResponseMessage(false, message));

            userVM.OrganizationId = OrgId;
            userVM.RoleId = 1;
            userVM.IsActive = false;
            user = mapper.Map<User>(userVM);
            await dbContext.Users.AddAsync(user);
            await SaveChangesAsync();
            ResponseMessage emailResponse = await emailRepository.SendMailAsync(user.Email, user.DisplayName, EmailCode.SendMailOnEmailVerification.ToString(), "generate verification link and add here");
            userVM = new UserVM
            {
                OrganizationId = OrgId,
                UserId = user.UserId,
                UserGuid = user.UserGuid,
                Email = user.Email,
                OrganizationVM = new OrganizationVM()
                {
                    OrganizationGuid = user.Organization.OrganizationGuid,
                    OrganizationId = user.OrganizationId,
                    OrganizationName = user.Organization.OrganizationName,
                    Email = user.Email
                },
                Message = emailResponse.Message
            };
            return (userVM, new ResponseMessage(true, AppMessages.AccountSuccess));
        }

        public async Task<ResponseMessage> CreateSocialMediaAccountAsync(UserVM userVM, OrganizationVM organizationVM)
        {
            User? user = await dbContext.Users.Where(col => col.Email == userVM.Email).FirstOrDefaultAsync();
            if (user != null)
            {
                user.ProfileImageUrl = userVM.ProfileImageUrl;
            }
            else
            {
                (int? OrgId, string message) = await CreateOrganizationAsync(organizationVM!);
                if (OrgId == null)
                    return new ResponseMessage(false, message);

                userVM.OrganizationId = OrgId;
                userVM.HashPassword = Guid.NewGuid().ToString();
                userVM.PasswordSalt = Guid.NewGuid().ToString();
                userVM.RoleId = 1;
                userVM.IsActive = true;
                user = mapper.Map<User>(userVM);
                await dbContext.Users.AddAsync(user);
            }
            await SaveChangesAsync();
            return new ResponseMessage(true, AppMessages.AccountSuccess);
        }

        public async Task<ResponseMessage> CreateOrganizationAsync(int userId, Guid userGuId, string securityKey, OrganizationVM organizationVM)
        {
            User? user = await dbContext.Users.FirstOrDefaultAsync(col => col.UserId == userId && col.UserGuid == userGuId && col.SecurityKey == securityKey);
            if (user != null)
                return new ResponseMessage(false, AppMessages.UnauthorizedAccess);

            (int? OrgId, string message) = await CreateOrganizationAsync(organizationVM!);
            if (OrgId == null)
                return new ResponseMessage(false, message);
            return new ResponseMessage(true, AppMessages.AccountSuccess);
        }

        public async Task<ResponseMessage> UpdateAccountType(int userId, string securityKey, bool isOrganization)
        {
            User? user = await dbContext!.Users.Where(col => col.UserId == userId && col.SecurityKey == securityKey && col.RoleId == 1).FirstOrDefaultAsync();
            if (user == null)
                return new ResponseMessage(false, AppMessages.UnauthorizedAccess);

            Organization organization = await dbContext.Organizations.FirstOrDefaultAsync(col => col.OrganizationId == user.OrganizationId) ?? throw new Exception(AppMessages.UnauthorizedAccess);
            organization.ModifiedDate = DateTime.UtcNow;
            organization.ModifiedUser = userId;
            await SaveChangesAsync();
            return new ResponseMessage(true, "Account updated successfully");
        }

        public async Task<(ResponseMessage, SignInSuccessResponse?)> SignInAsync(SignInRequest signInRequest)
        {
            User? user = await dbContext.Users.Include(rl => rl.Role).Include(org => org.Organization).Where(col => col.Username == signInRequest.UserName && col.HashPassword == signInRequest.Password && col.Organization.OrganizationStatusId == (byte)OrgStatus.Active).FirstOrDefaultAsync();
            if (user == null)
                return (new ResponseMessage(false, AppMessages.AuthenticationFailed), null);

            if (!user.Organization.IsEmailVerified)
                return (new ResponseMessage(false, AppMessages.EMAIL_VERIFICATION_ERROR), null);

            if (!user.IsActive)
                return (new ResponseMessage(false, AppMessages.UserSuspended), null);

            user.SecurityKey = signInRequest.SecurityKey;
            user.SecretKey = signInRequest.SecretKey;
            await SaveChangesAsync();

            SignInSuccessResponse successResponse = new()
            {
                UserId = user.UserId,
                UserGuid = user.UserGuid.ToString(),
                DisplayName = user.FirstName,
                SecurityKey = signInRequest.SecurityKey,
                SecretKey = signInRequest.SecretKey,
                OrganizationId = user.OrganizationId,
                OrganizationGuid = user.Organization.OrganizationGuid.ToString(),
                OrganizationName = user.Organization.OrganizationName,
                RoleName = user.Role.RoleName
            };

            return (new ResponseMessage(true, AppMessages.SUCCESS), successResponse);
        }

        //public async Task<ResponseMessage> ConvertIndividualToBusiness(int userId, string securityKey, OrganizationVM organization)
        //{
        //    User? user = await IsValidAdminUserAsync(userId, securityKey);
        //    if (user == null)
        //        return new ResponseMessage(false, AppMessages.UnauthorizedAccess);

        //    if(user.Organization != null)
        //        return new ResponseMessage(false, AppMessages.AlreadyConvertedToBusiness);

        //    (int? OrgId, string message) = await CreateOrganizationAsync(organization!);
        //    if (OrgId == null)
        //        return new ResponseMessage(false, message);
        //    user.OrganizationId = OrgId;
        //    user.IsOrganization = true;
        //    user.ModifiedDate = DateTime.UtcNow;
        //    user.ModifiedUser = userId;
        //    await SaveChangesAsync();
        //    return new ResponseMessage(true, AppMessages.ConvertedToBusinessSuccess);
        //}


    }
}
