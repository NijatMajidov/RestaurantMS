using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMS.Business.DTOs.ReservationDTOs;
using RMS.Business.Exceptions;
using RMS.Business.Services.Abstracts;
using RMS.Core.Entities;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly UserManager<AppUser> _userManager;
        public ReservationController(IReservationService reservationService, UserManager<AppUser> userManager)
        {
            _reservationService = reservationService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var reservs = await _reservationService.GetAllReservs(x => x.IsDeleted == false);
            return View(reservs);
        }
        public async Task<IActionResult> Create()
        {
            var tables = await _reservationService.GetAvailableTablesAsync();
            ViewBag.Tables = tables;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ReservCreateDto CreateDto)
        {
            if (!ModelState.IsValid)
            {
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View(CreateDto);
            }
            var user = await _userManager.GetUserAsync(User);
            
            try
            {
               await _reservationService.Create(CreateDto,user.Id);
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View();
            }
            catch (CountException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View();
            }
            catch(NameFormatException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View();
            }
            catch (PhoneFormatException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
