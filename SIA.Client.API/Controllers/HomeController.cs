using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SIA.Domain.Entities;
using SIA.Domain.Models;
using SIA.Infrastructure.Interfaces;
using System.Net;

namespace SIA.Client.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController(IUserRepository userRepository, IHttpContextAccessor contextAccessor) : ControllerBase
    {
        [HttpGet]
        [Route("start")]
        public ContentResult Start()
        {
            var request = contextAccessor.HttpContext?.Request;
            string logoUrl = $"{request?.Scheme}://{request?.Host}{request?.PathBase}/images/logo.png";

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.OK,
                ContentType = "text/html",
                Content = "Server is started"
            };
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> CreateAccount([FromBody] UserVM userVM)
        {
            ResponseMessage responseMessage = await userRepository.CreateSignUpAccountAsync(userVM);
            return Ok(responseMessage);
        }

        //public async Task<IActionResult> Login([FromBody] LoginRequest request)
        //{
        //    var user = await _userService.ValidateUserAsync(request.Username, request.Password);
        //    if (user == null) return Unauthorized();

        //    var accessToken = _tokenService.GenerateAccessToken(user);
        //    var refreshToken = _tokenService.GenerateRefreshToken();

        //    // Save refresh token in DB (to validate later)
        //    await _userService.SaveRefreshTokenAsync(user.Id, refreshToken);

        //    // Set refresh token in HttpOnly cookie
        //    Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Secure = true, // set true in production (HTTPS)
        //        SameSite = SameSiteMode.Strict, // prevents CSRF
        //        Expires = DateTime.UtcNow.AddDays(7)
        //    });

        //    return Ok(new { accessToken });
        //}

    }
}
