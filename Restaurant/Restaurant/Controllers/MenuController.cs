using Microsoft.AspNetCore.Mvc;
using RMS.Business.Services.Abstracts;

namespace Restaurant.Controllers
{
    public class MenuController : Controller
    {
        readonly IMenuItemService _menuItemService;
        public MenuController(IMenuItemService menuItem)
        {
            _menuItemService = menuItem;
        }
        public async Task<IActionResult> Index()
        {
            var menu = await _menuItemService.GetAllMenuItems();
            return View(menu);
        }
    }
}