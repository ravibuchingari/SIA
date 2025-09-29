using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SIA.Domain.Entities;
using SIA.Domain.Models;
using SIA.Infrastructure.Data;
using SIA.Infrastructure.DTO;
using SIA.Infrastructure.Interfaces;

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
            organization.SubscriptionId = (byte)SubscriptionPlans.Pro;
            organization.OrganizationStatusId = (byte)OrgStatus.EmailValidation;
            await dbContext.AddAsync(organization);
            await dbContext.SaveChangesAsync();
            return (organization.OrganizationId, "Success");
        }

        public async Task<(UserVM?, ResponseMessage)> CreateSignUpAccountAsync(UserVM userVM, OrganizationVM organizationVM)
        {
            User? user = await dbContext.Users.Include(org => org.Organization).FirstOrDefaultAsync(u => u.Username == userVM.Username);
            if (user != null)
                return (null, new ResponseMessage(false, AppMessages.DuplicateUsername));

            user = await dbContext.Users.Include(org => org.Organization).FirstOrDefaultAsync(u => u.Email == userVM.Email);
            if (user != null)
                return (null, new ResponseMessage(false, AppMessages.DuplicateEmail));

            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                (int? organizationId, string message) = await CreateOrganizationAsync(organizationVM!);
                if (organizationId == null)
                {
                    await transaction.RollbackAsync();
                    return (null, new ResponseMessage(false, message));
                }

               
                user = mapper.Map<User>(userVM);
                user.OrganizationId = organizationId ?? 0;
                user.RoleId = 1;
                user.IsActive = false;

                await dbContext.Users.AddAsync(user);
                await SaveChangesAsync();
                ResponseMessage emailResponse = await emailRepository.SendMailAsync(user.Email, user.DisplayName, EmailCode.SendMailOnEmailVerification.ToString(), "generate verification link and add here");
                userVM = new UserVM
                {
                    OrganizationId = user.OrganizationId,
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
                await transaction.CommitAsync();
                return (userVM, new ResponseMessage(true, AppMessages.AccountSuccess));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (null, new ResponseMessage(false, ex.Message));
            }
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
                (int? organiztionId, string message) = await CreateOrganizationAsync(organizationVM!);
                if (organiztionId == null)
                    return new ResponseMessage(false, message);

                userVM.OrganizationId = organiztionId ?? 0;
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

        public async Task<ResponseMessage> UpdateAccountTypeAsync(int userId, string securityKey, bool isOrganization)
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

        public async Task<string> GetSaltKeyAsync(string userName)
        {
            return await dbContext.Users.Where(col => col.Username == userName).Select(row => row.PasswordSalt).FirstOrDefaultAsync() ?? string.Empty;
        }

        public async Task<(ResponseMessage, SignInSuccessResponse?)> SignInAsync(SignInRequest signInRequest)
        {
            User? user = await dbContext.Users.Include(rl => rl.Role).Include(org => org.Organization).Where(col => col.Username == signInRequest.UserName && col.HashPassword == signInRequest.Password && col.IsDeleted == false && col.Organization.OrganizationStatusId != (byte)OrgStatus.Deleted).FirstOrDefaultAsync();
            if (user == null)
                return (new ResponseMessage(false, AppMessages.AuthenticationFailed), null);

            if (user.Organization.OrganizationStatusId == (byte)OrgStatus.Suspended)
                return (new ResponseMessage(false, AppMessages.AccountSuspended), null);

            if (!user.Organization.IsEmailVerified || user.Organization.OrganizationStatusId == (byte)OrgStatus.EmailValidation)
                return (new ResponseMessage(false, AppMessages.EMAIL_VERIFICATION_ERROR), null);

            if (!user.IsActive)
                return (new ResponseMessage(false, AppMessages.UserSuspended), null);

            user.SecurityKey = signInRequest.SecurityKey;
            user.SecretKey = signInRequest.SecretKey ?? string.Empty;
            await SaveChangesAsync();

            SignInSuccessResponse successResponse = new()
            {
                UserId = user.UserId,
                UserGuid = user.UserGuid.ToString(),
                DisplayName = user.FirstName,
                SecurityKey = signInRequest.SecurityKey ?? string.Empty,
                SecretKey = signInRequest.SecretKey ?? string.Empty,
                OrganizationId = user.OrganizationId,
                OrganizationGuid = user.Organization.OrganizationGuid.ToString(),
                OrganizationName = user.Organization.OrganizationName,
                RoleName = user.Role.RoleName
            };

            return (new ResponseMessage(true, AppMessages.SUCCESS), successResponse);
        }

        public async Task UpdateRefreshTokenAsync(RefreshTokenVM refreshTokenVM)
        {
            var refreshToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(col => col.UserId == refreshTokenVM.UserId);

            if (refreshToken == null)
            {
                var newToken = mapper.Map<RefreshToken>(refreshTokenVM);
                await dbContext.AddAsync(newToken);
            }
            else
            {
                mapper.Map(refreshTokenVM, refreshToken); 
            }

            await dbContext.SaveChangesAsync();

        }

        public async Task<bool> ValidateRefreshTokenAsync(int userId, string token)
        {
            return await dbContext.RefreshTokens.Where(col => col.UserId == userId && col.Token == token && col.Expires > DateTimeOffset.UtcNow).AnyAsync();
        }
    }
}
