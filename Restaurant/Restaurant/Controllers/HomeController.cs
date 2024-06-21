using Microsoft.AspNetCore.Mvc;
using RMS.Business.Services.Abstracts;

namespace Restaurant.Controllers
{
    public class HomeController : Controller
    {
        readonly ISlideService _service;
        public HomeController(ISlideService slideService)
        {
            _service = slideService;
        }
        public async Task<IActionResult> Index()
        {
            var Slide = await _service.GetAllSlides();
            return View(Slide);
        }
    }
}
