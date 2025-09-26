using Microsoft.AspNetCore.Mvc;
using SIA.Client.API.Models;
using SIA.Domain.Entities;
using SIA.Domain.Models;
using SIA.Infrastructure.Interfaces;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SIA.Client.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController(IUserRepository userRepository,
                                    ISharedRepository sharedRepository,
                                    IApiCallerRepository apiCallerRepository,
                                    IGlobalConfigRepository globalConfigRepository,
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
        public async Task<IActionResult> CreateAccount([FromBody] UserVM userVM)
        {
            ResponseMessage responseMessage = await userRepository.CreateSignUpAccountAsync(userVM);
            return Ok(responseMessage);
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
            userVM = await userRepository.CreateSocialMediaAccountAsync(userVM);
            return Ok(userVM);
        }
    }
}
