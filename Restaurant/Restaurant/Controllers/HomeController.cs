using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.ViewModels;
using RMS.Business.DTOs.EmployeeDTOs;
using RMS.Business.Services.Abstracts;
using RMS.Core.Entities;

namespace Restaurant.Controllers
{
    public class HomeController : Controller
    {
        readonly ISlideService _service;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(ISlideService slideService, UserManager<AppUser> userManager)
        {
            _service = slideService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var Slide = await _service.GetAllSlides();
            
            var users = await _userManager.Users.ToListAsync();
            var employeeDtos = new List<EmployeeGetDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Chefs"))
                {
                    var chefs = new EmployeeGetDto
                    {
                        UserId = user.Id,
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        UserName = user.UserName,
                        Roles = roles,
                        ImageUrl = user.ImageUrl,
                        Biography = user.Biography
                    };
                    employeeDtos.Add(chefs);
                }
            }
            var viewModel = new HomeViewModel
            {
                Slides = Slide,
                Employees = employeeDtos
            };
            
            return View(viewModel);
        }
    }
}
