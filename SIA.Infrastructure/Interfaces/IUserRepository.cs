
using SIA.Domain.Entities;
using SIA.Domain.Models;

namespace SIA.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<ResponseMessage> CreateOrganizationAsync(int userId, Guid userGuId, string securityKey, OrganizationVM organizationVM);
        Task<(UserVM?, ResponseMessage)> CreateSignUpAccountAsync(UserVM userVM, OrganizationVM organizationVM);
        Task<ResponseMessage> CreateSocialMediaAccountAsync(UserVM userVM, OrganizationVM organizationVM);
        Task<string> GetSaltKeyAsync(string userName);
        Task<(ResponseMessage, SignInSuccessResponse?)> SignInAsync(SignInRequest signInRequest);
        //Task<ResponseMessage> UpdateAccountType(int userId, string securityKey, bool isOrganization);
    }
}
