using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chefs()
        {
            return View();
        }
    }
}
