using Microsoft.AspNetCore.Mvc;
using SIA.Authentication;
using SIA.Client.API.Models;
using SIA.Domain.Entities;
using SIA.Domain.Models;
using SIA.Infrastructure.Interfaces;
using System.Net;
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
        [Route("signup")]
        public async Task<IActionResult> CreateSignUpAccount([FromBody] SignUpVM signUpVM)
        {
            byte[] saltBytes = DataProtection.GenerateRandomNumber(30);
            byte[] hashPassword = DataProtection.GetSaltHasPassword(Encoding.ASCII.GetBytes(signUpVM.UserVM.HashPassword), saltBytes);
            signUpVM.UserVM.HashPassword = DataProtection.EncryptWithIV(Convert.ToBase64String(hashPassword), AppConstants.ORG_AES_KEY_AND_IV);
            signUpVM.UserVM.PasswordSalt = DataProtection.EncryptWithIV(Convert.ToBase64String(saltBytes), AppConstants.ORG_AES_KEY_AND_IV);
            (UserVM? userVM, ResponseMessage responseMessage) = await userRepository.CreateSignUpAccountAsync(signUpVM.UserVM, signUpVM.OrganizationVM);
            if (responseMessage.IsSuccess && userVM != null)
            {
                return Ok(userVM);
            }
            else
                return BadRequest(responseMessage);
        }

        [HttpPost]
        [Route("signin/google/validation")]
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
                IsEmailVerified = userInfo.email_verified
            };
            ResponseMessage responseMessage = await userRepository.CreateSocialMediaAccountAsync(userVM, new OrganizationVM());
            if (responseMessage.IsSuccess)
                return Ok(responseMessage);
            else
                return BadRequest(responseMessage.Message);
        }

        [HttpPost]
        [Route("org/create/{userId}/{userGuId}/{securityKey}")]
        public async Task<IActionResult> CreateOrganization([FromRoute] string userId, [FromRoute] string userGuId, [FromRoute] string securityKey, [FromBody] OrganizationVM organizationVM)
        {
            ResponseMessage responseMessage = await userRepository.CreateOrganizationAsync(int.Parse(DataProtection.UrlDecode(userId, AppConstants.ORG_AES_KEY_AND_IV)), DataProtection.StringToGuid(userGuId), securityKey, organizationVM);
            return responseMessage.IsSuccess ? Ok(responseMessage) : BadRequest(responseMessage.Message);
        }


        [HttpPost]
        [Route("signin/user")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest signInRequest)
        {
            signInRequest.SecurityKey = Guid.NewGuid().ToString();
            signInRequest.SecretKey = DataProtection.UrlEncode(Guid.NewGuid().ToString(), AppConstants.ORG_AES_KEY_AND_IV);
            string passwordSalt = await userRepository.GetSaltKeyAsync(signInRequest.UserName);
            passwordSalt = DataProtection.DecryptWithIV(passwordSalt, AppConstants.ORG_AES_KEY_AND_IV);

            byte[] hashPassword = DataProtection.GetSaltHasPassword(Encoding.ASCII.GetBytes(signInRequest.Password), Convert.FromBase64String(passwordSalt));
            signInRequest.Password = DataProtection.EncryptWithIV(Convert.ToBase64String(hashPassword), AppConstants.ORG_AES_KEY_AND_IV);

            (ResponseMessage responseMessage, SignInSuccessResponse? successResponse) = await userRepository.SignInAsync(signInRequest);
            if (responseMessage.IsSuccess && successResponse != null)
            {
                TokenResponse tokenResponse = await jwtTokenHandler.GenerateTokenAsync(successResponse.UserId.ToString(), successResponse.UserGuid.ToString(), successResponse.RoleName, successResponse.SecurityKey);
                if (tokenResponse.IsSuccess)
                {
                    successResponse.AccessToken = tokenResponse.JwtToken;
                    //call efcore to strore
                    return Ok(tokenResponse);
                }
                else
                    return BadRequest(tokenResponse.Message);
            }
            else
                return BadRequest(responseMessage.Message);
        }
    }
}
