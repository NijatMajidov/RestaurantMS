using Microsoft.AspNetCore.Mvc;
using RMS.Business.Services.Abstracts;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService category)
        {
            _categoryService = category;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategories(x=>x.IsDeleted==false,x=>x.Name);
            return View(categories);
        }
    }
}
