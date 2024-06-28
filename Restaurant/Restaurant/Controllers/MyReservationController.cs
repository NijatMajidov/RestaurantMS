using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMS.Business.DTOs.ReservationDTOs;
using RMS.Business.Services.Abstracts;
using RMS.Core.Entities;
using RMS.Data.Repositories.Abstractions;

namespace Restaurant.Controllers
{
    [Authorize]
    public class MyReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITableService _tableService;
        //private readonly IMapper _mapper;
        //private readonly IReservationRepository _reservationRepository;
        public MyReservationController(IReservationService reservationService, UserManager<AppUser> userManager,IReservationRepository reservationRepository, ITableService tableService, IMapper mapper)
        {
            _reservationService = reservationService;
            
            _userManager = userManager;
            _tableService = tableService;
            //_mapper = mapper;_reservationRepository = reservationRepository;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var reservs = await _reservationService.GetAllReservs(x => x.UserId == user.Id && x.IsDeleted==false);
            return View(reservs);
        }
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    var entity = await _reservationService.GetReserv(x => x.UserId == user.Id && x.Id ==id);
        //    var reserv = _mapper.Map<Reservation>(entity);

        //    reserv.IsDeleted = true;
        //    await _reservationRepository.CommitAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
