using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RMS.Business.Exceptions;
using RMS.Business.Exceptions.TableEx;
using RMS.Business.Services.Abstracts;
using RMS.Business.Services.Concretes;
using RMS.Core.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MenuController : Controller
    {
        private readonly IMenuItemService _menuItemService;
        private readonly ICategoryService _categoryService;

        public MenuController(IMenuItemService menuItemService, ICategoryService categoryService)
        {
            _menuItemService = menuItemService;
            _categoryService = categoryService;
        }
        public async Task< IActionResult> Index()
        {
            var menuItems = await _menuItemService.GetAllMenuItems(x=>x.IsDeleted==false);
            return View(menuItems);
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _menuItemService.GetAvailableCategoryAsync();
            if (categories == null || !categories.Any())
            {
                ModelState.AddModelError("", "No categories found. Please create a category first.");
                return View();
            }
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuItem menuItem)
            {
            if (!ModelState.IsValid)
            {
                var categories = await  _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
            }
            try
            {
                await _menuItemService.Create(menuItem);
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
            }
            catch (RMS.Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
            }
            catch (FileContentypeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
            }
            catch (PriceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
            }
            catch (NameSizeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
            }
            catch (NameFormatException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
            }
           
            return RedirectToAction(nameof(Index));
            
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return View("Error");

            var menuItem = await _menuItemService.GetMenuItem(x => x.Id == id);
            if (menuItem == null) return View("Error");

            var categories = await _menuItemService.GetAvailableCategoryAsync();
            if (categories == null || !categories.Any())
            {
                ModelState.AddModelError("", "No categories found. Please create a category first.");
                return View(menuItem);
            }
            ViewBag.Categories = categories;
            return View(menuItem);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, MenuItem menuItem)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewData["Categories"] = categories;
                return View(menuItem);
            }
            try
            {
                await _menuItemService.Update(menuItem.Id, menuItem);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View("Error");
                
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
                
            }
            catch (RMS.Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
                
            }
            catch (FileContentypeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
                
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
                
            }
            catch (PriceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
                
            }
            catch (NameSizeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
            }
            catch (NameFormatException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
                
            }
            
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var categories = await _menuItemService.GetAvailableCategoryAsync();
                ViewBag.Categories = categories;
                return View(menuItem);
                
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var exsist = _menuItemService.GetMenuItem(x => x.Id == id);
            if (exsist == null) return View("Error");

            try
            {
                await _menuItemService.SoftDeleteMenuItem(id);
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View("Error");
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View("Error");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }
    }

}
