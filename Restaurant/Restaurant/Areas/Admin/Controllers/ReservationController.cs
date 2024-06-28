using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly ITableService _tableService;
        public ReservationController(IReservationService reservationService, UserManager<AppUser> userManager, ITableService tableService)
        {
            _reservationService = reservationService;
            _userManager = userManager;
            _tableService = tableService;
        }
        [Authorize(Roles = "Admin,Waiter")]
        public async Task<IActionResult> Index()
        {
            var reservs = await _reservationService.GetAllReservs(x => x.IsDeleted == false);
            return View(reservs);
        }
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account",""); 
            }
            ViewBag.User = user;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservCreateDto CreateDto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
            {
                if (user == null)
                {
                    return RedirectToAction("Login", "Account", "");
                }
                ViewBag.User = user;
                return View(CreateDto);
            }
            TempData["ReservData"] = JsonConvert.SerializeObject(CreateDto);
            return RedirectToAction("SelectTable", new {date = CreateDto.ReservationDate,time = CreateDto.ReservationTime});
        }
        public async Task<IActionResult> SelectTable(DateTime? date, TimeSpan time)
        {
            var reservations = await _reservationService.GetAllReservs();
            List<int> reservedTableIds = reservations
                .Where(r => r.TableId.HasValue &&
                            r.ReservationDate == date &&
                            Math.Abs((r.ReservationTime - time).TotalHours) < 2)
                .Select(r => r.TableId.Value)
                .ToList();

            var tables = await _reservationService.GetAvailableTablesAsync();
            var availableTables = tables
                .Where(t => !reservedTableIds.Contains(t.Id))
                .ToList();

            ViewBag.Tables = availableTables;

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", "");
            }
            ViewBag.User = user;

            if (TempData["ReservData"] == null)
            {
                return RedirectToAction("Create");
            }

            var reservationData = JsonConvert.DeserializeObject<ReservCreateDto>((string)TempData["ReservData"]);
            return View(reservationData);
        }
        [HttpPost]
        public async Task<IActionResult> SelectTable(ReservCreateDto CreateDto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", "");
            }
            if (!ModelState.IsValid)
            {
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                ViewBag.User = user;
                return View(CreateDto);
            }
            try
            {
               await _reservationService.Create(CreateDto,user.Id);
               return RedirectToAction(nameof(Index));
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View(CreateDto);
            }
            catch (CountException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View(CreateDto);
            }
            catch(NameFormatException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View(CreateDto);
            }
            catch (PhoneFormatException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View(CreateDto);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View(CreateDto);
            }
            catch(TimeConflictException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View(CreateDto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View(CreateDto);
            }
        }
        [Authorize(Roles = "Admin,Waiter")]
        public async Task<IActionResult> Update(int id)
        {
            if (id == 0) return View("error");
            var reservation = await _reservationService.GetReserv(r => r.Id == id);
            if (reservation == null)
            {
                return View("error");
            }
            var updateDto = new ReservUpdateDto
            {
                Id = id,
                ReservationDate = reservation.ReservationDate,
                ReservationTime = reservation.ReservationTime,
                NumberOfGuests = reservation.NumberOfGuests,
                Note = reservation.Note,
                TableId = reservation.TableId
            };
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", "");
            }
            ViewBag.User = user;
            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ReservUpdateDto updateDto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
            {
                if (user == null)
                {
                    return RedirectToAction("Login", "Account", "");
                }
                ViewBag.User = user;
                return View(updateDto);
            }
            TempData["UpdateReservData"] = JsonConvert.SerializeObject(updateDto);
            return RedirectToAction("SelectUpdateTable", new { date = updateDto.ReservationDate, time = updateDto.ReservationTime });
        }
        public async Task<IActionResult> SelectUpdateTable(DateTime date, TimeSpan time)
        {
            var reservations = await _reservationService.GetAllReservs();
            List<int> reservedTableIds = reservations
                .Where(r => r.TableId.HasValue &&
                            r.ReservationDate == date &&
                            Math.Abs((r.ReservationTime - time).TotalHours) < 2)
                .Select(r => r.TableId.Value)
                .ToList();

            var tables = await _reservationService.GetAvailableTablesAsync();
            var availableTables = tables
                .Where(t => !reservedTableIds.Contains(t.Id))
                .ToList();

            ViewBag.Tables = availableTables;

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", "");
            }
            ViewBag.User = user;

            if (TempData["UpdateReservData"] == null)
            {
                return RedirectToAction("Update");
            }

            var reservationData = JsonConvert.DeserializeObject<ReservUpdateDto>((string)TempData["UpdateReservData"]);
            return View(reservationData);
        }

        [HttpPost]
        public async Task<IActionResult> SelectUpdateTable(ReservUpdateDto updateDto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", "");
            }
            if (!ModelState.IsValid)
            {
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                ViewBag.User = user;
                return View(updateDto);
            }
            try
            {
                await _reservationService.Update(updateDto.Id, updateDto);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View(updateDto);
            }
            catch (CountException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View(updateDto);
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View(updateDto);
            }
            catch (TimeConflictException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View(updateDto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var tables = await _reservationService.GetAvailableTablesAsync();
                ViewBag.Tables = tables;
                return View(updateDto);
            }
        }
        [Authorize(Roles = "Admin,Waiter")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _reservationService.DeleteRezerv(id);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
