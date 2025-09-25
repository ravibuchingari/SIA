using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace SIA.Authentication
{
    public static class GoogleAuthenticationExtension
    {
        public static IServiceCollection AddGoogleAuthentication(this IServiceCollection services, string clientId, string secretKey)
        {
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = clientId;
                    options.ClientSecret = secretKey;
                    options.SaveTokens = true;
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                });

            return services;
        }
    }

}
