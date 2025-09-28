using Microsoft.Extensions.Configuration;
using SIA.Domain.Entities;
using SIA.Infrastructure.Interfaces;

namespace SIA.Infrastructure.Repositories
{
    public class GlobalConfigRepository(IConfiguration configuration) : IGlobalConfigRepository
    {
        public async Task<AuthConfigVM?> GetAuthConfigAsync(string provider)
        {
            IConfigurationSection providerSection = configuration.GetSection($"Authentication:{provider}");
            if (providerSection != null)
            {
                AuthConfigVM authConfigVM = new()
                {
                    ClientId = providerSection["ClientId"],
                    ClientSecretKey = providerSection["ClientSecretKey"]
                };
                authConfigVM.ClientSecretKey = providerSection["ClientSecretKey"];
                return await Task.FromResult(authConfigVM);
            }
            else
                return null;
        }
    }
}
