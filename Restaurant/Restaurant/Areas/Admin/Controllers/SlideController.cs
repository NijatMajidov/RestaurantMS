using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMS.Business.Exceptions;
using RMS.Business.Services.Abstracts;
using RMS.Business.DTOs.SlideDTOs;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SlideController : Controller
    {
        readonly ISlideService _slideService;
        public SlideController(ISlideService slideService)
        {
            _slideService = slideService;
        }
        public async Task<IActionResult> Index()
        {
            var tables = await _slideService.GetAllSlides(x => x.IsDeleted == false);
            return View(tables);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SlideCreateDto CreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _slideService.Create(CreateDTO);
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (RMS.Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (FileContentypeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (NameSizeException ex)
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
            SlideUpdateDto table;
            try
            {
                table = await _slideService.GetSlideForUpdate(id);
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

            return View(table);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SlideUpdateDto UpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _slideService.Update(UpdateDTO.Id, UpdateDTO);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View("Error");
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (RMS.Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (FileContentypeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (NameSizeException ex)
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
        public async Task<IActionResult> Delete(int id)
        {
            var exsist = _slideService.GetSlide(x => x.Id == id);
            if (exsist == null) return View("Error");

            try
            {
                await _slideService.SoftDeleteSlide(id);
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
