
using SIA.Domain.Entities;
using SIA.Domain.Models;

namespace SIA.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<ResponseMessage> CreateSignUpAccountAsync(UserVM userVM, OrganizationVM? organizationVM);
        Task<UserVM> CreateSocialMediaAccountAsync(UserVM userVM);
        Task<ResponseMessage> UpdateAccountType(int userId, string securityKey, bool isOrganization, OrganizationVM? organization);
    }
}
