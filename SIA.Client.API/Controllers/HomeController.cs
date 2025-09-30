using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SIA.Authentication;
using SIA.Client.API.Models;
using SIA.Domain.Entities;
using SIA.Domain.Models;
using SIA.Infrastructure.DTO;
using SIA.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SIA.Client.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController(IUserRepository userRepository,
                                    ISharedRepository sharedRepository,
                                    IApiCallerRepository apiCallerRepository,
                                    IGlobalConfigRepository globalConfigRepository,
                                    IJwtTokenHandler jwtTokenHandler,
                                    IHttpContextAccessor contextAccessor) : ControllerBase
    {
        //LinkGenerator linkGenerator,
        public void SetCookie(string cookieName, string cookieValue, DateTimeOffset expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = expires
            };
            Response.Cookies.Append(cookieName, cookieValue, cookieOptions);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenHandler.GetJwtSecurityKey())),
                ValidateLifetime = false // ignore expiration
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet]
        [Route("start")]
        public ContentResult Start()
        {
            var request = contextAccessor.HttpContext?.Request;
            string logoUrl = $"{request?.Scheme}://{request?.Host}{request?.PathBase}/images/logo.png";
            string message = new StringBuilder().Append("<html><head><title>App Server</title></head>")
                                                .Append("<body style='background-color: black;'><div><img style='position: absolute; left: 50%; top: 50%; transform: translate(-50%,-50%);' src='")
                                                .Append(logoUrl)
                                                .Append("'>")
                                                .Append("<div style='color: white; font-family: Verdana, Geneva, Tahoma;'>App server is running...</div>")
                                                .Append("</div></body></html>").ToString();

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.OK,
                ContentType = "text/html",
                Content = message
            };
        }

        [HttpGet]
        [Route("signup/utilities")]
        public async Task<IActionResult> GetSignUpUtilities()
        {
            SignUpUtilities signUpUtilities = new()
            {
                Languages = await sharedRepository.GetLanguagesAsync(),
                TimeZones = await sharedRepository.GetTimeZonesAsync()
            };
            return Ok(signUpUtilities);
        }

        [HttpPost]
        [Route("signup/account")]
        public async Task<IActionResult> CreateSignUpAccount([FromBody] SignUpVM signUpVM)
        {
            byte[] saltBytes = DataProtection.GenerateRandomNumber(30);
            byte[] hashPassword = DataProtection.GetSaltHasPassword(Encoding.ASCII.GetBytes(signUpVM.User.HashPassword), saltBytes);
            signUpVM.User.HashPassword = DataProtection.EncryptWithIV(Convert.ToBase64String(hashPassword), AppConstants.ORG_AES_KEY_AND_IV);
            signUpVM.User.PasswordSalt = DataProtection.EncryptWithIV(Convert.ToBase64String(saltBytes), AppConstants.ORG_AES_KEY_AND_IV);
            (ResponseMessage responseMessage, UserVM? userVM) = await userRepository.CreateSignUpAccountAsync(signUpVM.User, signUpVM.Organization);
            if (responseMessage.IsSuccess && userVM != null)
            {
                //send email verification link
                return Ok(userVM);
            }
            else
                return BadRequest(responseMessage);
        }

        [HttpPost]
        [Route("signin/google/authentication")]
        public async Task<IActionResult> SignInWithGooggleValidation([FromBody] GoogleAuthVM credential)
        {
            AuthConfigVM? authConfigVM = await globalConfigRepository.GetAuthConfigAsync(SIA.Domain.Models.Providers.Google.ToString());
            if (authConfigVM == null)
                return BadRequest(AppMessages.ProviderDeactivated);

            GoogleUserInfoVM? userInfo = await apiCallerRepository.GetAsync<GoogleUserInfoVM>(authConfigVM.UserinfoApi!, credential.access_token);
            if (userInfo == null)
                return Unauthorized(AppMessages.GoogleUserVerificationFailed);
            UserVM userVM = new()
            {
                SocialAuthId = userInfo.sub,
                Username = userInfo.email,
                FirstName = userInfo.given_name,
                LastName = userInfo.family_name,
                DisplayName = userInfo.name,
                ProfileImageUrl = userInfo.picture,
                Email = userInfo.email,
                IsEmailVerified = userInfo.email_verified,
                SecurityKey = Guid.NewGuid().ToString(),
                SecretKey = DataProtection.EncryptWithIV(Guid.NewGuid().ToString(), AppConstants.ORG_AES_KEY_AND_IV)
            };

            OrganizationVM organizationVM = new()
            {
                OrganizationName = userInfo.name,
                Email = userInfo.email,
            };

            (ResponseMessage responseMessage, SignInSuccessResponse? successResponse) = await userRepository.CreateSocialMediaAccountAsync(userVM, organizationVM);
            if (successResponse != null && responseMessage.IsSuccess)
            {
                TokenResponse tokenResponse = await jwtTokenHandler.GenerateTokenAsync(successResponse.UserId.ToString(), successResponse.UserGuid.ToString(), successResponse.Email, successResponse.RoleName, successResponse.SecurityKey);
                if (tokenResponse.IsSuccess)
                {
                    successResponse.SecretKey = DataProtection.DecryptWithIV(successResponse.SecretKey, AppConstants.ORG_AES_KEY_AND_IV);
                    await userRepository.UpdateRefreshTokenAsync(tokenResponse.RefreshToken);
                    SetCookie(AppMessages.COOKIE_REFRESH_TOKEN, DataProtection.EncryptWithIV(tokenResponse.RefreshToken.Token, AppConstants.ORG_AES_KEY_AND_IV), tokenResponse.RefreshToken.Expires);
                    successResponse.AccessToken = tokenResponse.AccessToken;
                    successResponse.RefreshKey = DataProtection.UrlEncode(tokenResponse.RefreshToken.Token, AppConstants.ORG_AES_KEY_AND_IV);
                    return Ok(successResponse);
                }
                else
                    return BadRequest(tokenResponse.Message);
            }
            else
                return BadRequest(responseMessage.Message);
        }

        //[HttpPost]
        //[Route("org/create/{userId}/{userGuId}/{securityKey}")]
        //public async Task<IActionResult> CreateOrganization([FromRoute] string userId, [FromRoute] string userGuId, [FromRoute] string securityKey, [FromBody] OrganizationVM organizationVM)
        //{
        //    ResponseMessage responseMessage = await userRepository.CreateOrganizationAsync(int.Parse(DataProtection.UrlDecode(userId, AppConstants.ORG_AES_KEY_AND_IV)), DataProtection.StringToGuid(userGuId), securityKey, organizationVM);
        //    return responseMessage.IsSuccess ? Ok(responseMessage) : BadRequest(responseMessage.Message);
        //}

        [HttpPost]
        [Route("signin/email/authentication")]
        [ProducesResponseType(typeof(SignInSuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest signInRequest)
        {
            signInRequest.SecurityKey = Guid.NewGuid().ToString();
            signInRequest.SecretKey = DataProtection.EncryptWithIV(Guid.NewGuid().ToString(), AppConstants.ORG_AES_KEY_AND_IV);
            string passwordSalt = await userRepository.GetSaltKeyAsync(signInRequest.UserName);
            passwordSalt = DataProtection.DecryptWithIV(passwordSalt, AppConstants.ORG_AES_KEY_AND_IV);

            byte[] hashPassword = DataProtection.GetSaltHasPassword(Encoding.ASCII.GetBytes(signInRequest.Password), Convert.FromBase64String(passwordSalt));
            signInRequest.Password = DataProtection.EncryptWithIV(Convert.ToBase64String(hashPassword), AppConstants.ORG_AES_KEY_AND_IV);

            (ResponseMessage responseMessage, SignInSuccessResponse? successResponse) = await userRepository.SignInAsync(signInRequest);
            if (responseMessage.IsSuccess && successResponse != null)
            {
                TokenResponse tokenResponse = await jwtTokenHandler.GenerateTokenAsync(successResponse.UserId.ToString(), successResponse.UserGuid.ToString(), successResponse.Email, successResponse.RoleName, successResponse.SecurityKey);
                if (tokenResponse.IsSuccess)
                {
                    successResponse.SecretKey = DataProtection.DecryptWithIV(successResponse.SecretKey, AppConstants.ORG_AES_KEY_AND_IV);
                    await userRepository.UpdateRefreshTokenAsync(tokenResponse.RefreshToken);
                    SetCookie(AppMessages.COOKIE_REFRESH_TOKEN, DataProtection.EncryptWithIV(tokenResponse.RefreshToken.Token, AppConstants.ORG_AES_KEY_AND_IV), tokenResponse.RefreshToken.Expires);
                    successResponse.AccessToken = tokenResponse.AccessToken;
                    successResponse.RefreshKey = DataProtection.UrlEncode(tokenResponse.RefreshToken.Token, AppConstants.ORG_AES_KEY_AND_IV);
                    return Ok(successResponse);
                }
                else
                    return BadRequest(tokenResponse.Message);
            }
            else
                return BadRequest(responseMessage.Message);
        }


        [HttpPost()]
        [Route("token/refresh/request")]
        [ProducesResponseType(typeof(AuthenticationToken), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh([FromBody] AuthenticationToken tokenRequest)
        {
            tokenRequest.RefreshToken = DataProtection.UrlDecode(tokenRequest.RefreshToken ?? string.Empty, AppConstants.ORG_AES_KEY_AND_IV);
            string refreshTokenCookie = DataProtection.DecryptWithIV(Request.Cookies[AppMessages.COOKIE_REFRESH_TOKEN] ?? string.Empty, AppConstants.ORG_AES_KEY_AND_IV);
            
            if(tokenRequest.RefreshToken != refreshTokenCookie)
                return Unauthorized(AppMessages.UnauthorizedAccess);

            var principal = GetPrincipalFromExpiredToken(tokenRequest.AccessToken);
            if (principal == null) return BadRequest("Invalid access token");
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(string.IsNullOrEmpty(userId))
                return Unauthorized(AppMessages.UnauthorizedAccess);

            bool isValid = await userRepository.ValidateRefreshTokenAsync(int.Parse(userId), tokenRequest.RefreshToken);
            if (!isValid) return Unauthorized(AppMessages.UnauthorizedAccess);

            TokenResponse tokenResponse = await jwtTokenHandler.GenerateTokenByClaimsAcync(principal.Claims);

            await userRepository.UpdateRefreshTokenAsync(tokenResponse.RefreshToken);
            SetCookie(AppMessages.COOKIE_REFRESH_TOKEN, DataProtection.EncryptWithIV(tokenResponse.RefreshToken.Token, AppConstants.ORG_AES_KEY_AND_IV), tokenResponse.RefreshToken.Expires);

            AuthenticationToken authenticationToken = new()
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = DataProtection.UrlEncode(tokenResponse.RefreshToken.Token, AppConstants.ORG_AES_KEY_AND_IV)
            };
            return Ok(authenticationToken);
        }
    }
}
