using Microsoft.AspNetCore.Mvc;

namespace SIA.Admin.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
