using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMS.Business.Exceptions;
using RMS.Business.Services.Abstracts;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RestaurantController : Controller
    {
        readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        public async Task<IActionResult> Index()
        {
            var restaurant = await _restaurantService.GetAllTables(x => x.IsDeleted == false);
            return View(restaurant);
            
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RMS.Core.Entities.Restaurant categoryCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _restaurantService.Create(categoryCreateDTO);
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (NameSizeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (NameFormatException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (PhoneFormatException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            RMS.Core.Entities.Restaurant category;
            try
            {
                category = await _restaurantService.GetTable(x=>x.Id==id);
            }
            catch (EntityNotFoundException ex)
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
        public async Task<IActionResult> Update(RMS.Core.Entities.Restaurant UpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _restaurantService.Update(UpdateDTO.Id, UpdateDTO);
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (NameSizeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (PhoneFormatException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (NameFormatException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (EntityNotFoundException ex)
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
            var exsist = _restaurantService.GetTable(x => x.Id == id);
            if (exsist == null) return View("Error");

            try
            {
                await _restaurantService.SoftDeleteTable(id);
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
