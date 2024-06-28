using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMS.Core.Entities;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Waiter")]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public DashboardController(UserManager<AppUser> user)
        {
            _userManager = user;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UserInfo()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }
    }
    
}
