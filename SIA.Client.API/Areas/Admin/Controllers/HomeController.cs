using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIA.Client.API.Models;

namespace SIA.Client.API.Areas.Admin.Controllers
{
    [Area(AppConstants.ROLE_ADMIN)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            return Ok("Admin API is running...");
        }
    }
}
