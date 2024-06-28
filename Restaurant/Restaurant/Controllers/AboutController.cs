using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Business.DTOs.EmployeeDTOs;
using RMS.Business.Services.Abstracts;
using RMS.Core.Entities;

namespace Restaurant.Controllers
{
    public class AboutController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRestaurantService _restaurantService;
        public AboutController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IRestaurantService restaurantService
           )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _restaurantService = restaurantService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Chefs()
        {
            var users = await _userManager.Users.ToListAsync();
            var employeeDtos = new List<EmployeeGetDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Chefs"))
                {
                    var employeeDto = new EmployeeGetDto
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
                    employeeDtos.Add(employeeDto);
                }
            }

            return View(employeeDtos);
        }
        public async Task<IActionResult> Waiter()
        {
            var users = await _userManager.Users.ToListAsync();
            var employeeDtos = new List<EmployeeGetDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Waiter"))
                {
                    var employeeDto = new EmployeeGetDto
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
                    employeeDtos.Add(employeeDto);
                }
            }

            return View(employeeDtos);
        }
        public async Task<IActionResult> ContactUs()
        {
            var ContactUsDto = await _restaurantService.GetTable();
            return View(ContactUsDto);
        }
        public IActionResult Guestbook()
        {
            return View();
        }
    }
}
