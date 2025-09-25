using Microsoft.AspNetCore.Mvc;
using SIA.Client.API.Models;

namespace SIA.Client.API.Areas.User.Controllers
{
    [Area(AppConstants.ROLE_USER)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
    }
}
