using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SIA.Client.API.Models;
using SIA.Domain.Entities;
using SIA.Domain.Models;
using SIA.Infrastructure.Interfaces;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace SIA.Client.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController(IUserRepository userRepository,
                                    ISharedRepository sharedRepository,
                                    IConfiguration configuration,
                                    IApiCallerRepository apiCallerRepository,
                                    IHttpContextAccessor contextAccessor) : ControllerBase
    {
        //LinkGenerator linkGenerator,
        public void SetCookie(string cookieName, string cookieValue, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = expires
            };
            //DateTimeOffset.UtcNow.AddDays(7)
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
        [Route("test")]
        public IActionResult Test([FromQuery] string search, [FromQuery] int page)
        {
            return Ok(new ResponseMessage(isSuccess: true, message: "success"));
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

        //[HttpGet]
        //[Route("signin/google")]
        //public IActionResult SignInWithGooggle([FromQuery] string returnUrl)
        //{
        //    string? redirectUrl = linkGenerator.GetUriByAction(HttpContext,"SignInWithGooggleCallback", "Home", new { returnUrl });
        //    var props = new AuthenticationProperties() { RedirectUri = redirectUrl };
        //    return Challenge(props, "Google");
        //}

        [HttpPost]
        [Route("signin/google/validation")]
        public async Task<IActionResult> SignInWithGooggleValidation([FromBody] GoogleAuthVM credential)
        {
            try
            {
                IConfigurationSection googleAuthNSection = configuration.GetSection("Authentication:Google");
                GoogleUserInfoVM? userInfo = await apiCallerRepository.GetAsync<GoogleUserInfoVM>(googleAuthNSection["UserinfoApi"]!, credential.access_token);
                if (userInfo == null)
                    return Unauthorized(AppMessages.GoogleUserVerificationFailed);
                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
