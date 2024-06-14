using Microsoft.AspNetCore.Mvc;
using RMS.Business.DTOs.CategoryDTOs;
using RMS.Business.Services.Abstracts;
using System.Text.RegularExpressions;

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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDTO categoryCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _categoryService.Create(categoryCreateDTO);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetCategoryForUpdate(id);
            if (category == null)
            {
                return View("Error");
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDTO categoryUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _categoryService.Update(categoryUpdateDTO.Id,categoryUpdateDTO);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _categoryService.SoftDeleteCategory(id);
            }
            catch (FileNotFoundException ex)
            {
                ModelState.AddModelError("",ex.Message);
                return View("Error");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
