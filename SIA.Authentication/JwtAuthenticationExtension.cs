using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Authentication.JWTAuthenticationManager
{
    public static class JwtAuthenticationExtension
    {
        public static void AddJwtAuthenticationExtension(this IServiceCollection services, JwtTokenParameter jwtTokenParameter)
        {
            services.AddAuthorization();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.RequireHttpsMetadata = false;
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = jwtTokenParameter.IsValidateIssuer,
                    ValidateAudience = jwtTokenParameter.IsValidateAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenParameter.JwtSecurityKey))
                };

                option.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        if (!string.IsNullOrEmpty(accessToken) && context.HttpContext.Request.Path.StartsWithSegments("/device-hub"))
                            context.Token = accessToken;
                        return Task.CompletedTask;
                    }
                };
            });

            //.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            //{
            //    options.ClientId = jwtTokenParameter.GoogleClientId;
            //    options.ClientSecret = jwtTokenParameter.GoogleSecretKey;
            //    options.ClaimActions.MapJsonKey("urn:google:picture", "picture","url");
            //});
        }
    }
}
