
using SIA.Domain.Entities;
using SIA.Domain.Models;

namespace SIA.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        //Task<ResponseMessage> CreateOrganizationAsync(int userId, Guid userGuId, string securityKey, OrganizationVM organizationVM);
        Task<(ResponseMessage, UserVM?)> CreateSignUpAccountAsync(UserVM userVM, OrganizationVM organizationVM);
        Task<(ResponseMessage, SignInSuccessResponse?)> CreateSocialMediaAccountAsync(UserVM userVM, OrganizationVM organizationVM);
        Task<string> GetSaltKeyAsync(string userName);
        Task<(ResponseMessage, SignInSuccessResponse?)> SignInAsync(SignInRequest signInRequest);
        Task UpdateRefreshTokenAsync(RefreshTokenVM refreshTokenVM);
        Task<bool> ValidateRefreshTokenAsync(int userId, string token);
    }
}
