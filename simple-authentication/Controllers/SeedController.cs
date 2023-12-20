using Microsoft.AspNetCore.Mvc;

namespace simple_authentication.Controllers
{
    public class SeedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
