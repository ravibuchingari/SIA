
using SIA.Domain.Entities;
using SIA.Domain.Models;

namespace SIA.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<ResponseMessage> CreateOrganizationAsync(int userId, Guid userGuId, string securityKey, OrganizationVM organizationVM);
        Task<ResponseMessage> CreateSignUpAccountAsync(UserVM userVM, OrganizationVM organizationVM);
        Task<ResponseMessage> CreateSocialMediaAccountAsync(UserVM userVM, OrganizationVM organizationVM);
        Task<ResponseMessage> UpdateAccountType(int userId, string securityKey, bool isOrganization);
    }
}
