using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMS.Business.DTOs.CategoryDTOs;
using RMS.Business.Exceptions;
using RMS.Business.Exceptions.CategoryEx;
using RMS.Business.Services.Abstracts;
using System.Text.RegularExpressions;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
            try
            {
                await _categoryService.Create(categoryCreateDTO);
            }
            catch(EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (DuplicateException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            CategoryUpdateDTO category;
            try
            {
                category = await _categoryService.GetCategoryForUpdate(id);
            }
            catch (CategoryNotFound ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View("Error");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
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
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (DuplicateException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch(CategoryNotFound ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View("Error");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var exsist = _categoryService.GetCategory(x => x.Id == id);
            if (exsist == null) return View("Error");
            
            try
            {
                await _categoryService.SoftDeleteCategory(id);
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError("",ex.Message);
                return View("Error");
            }
            catch (CategoryNotFound ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
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
