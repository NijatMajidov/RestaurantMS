using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Business.DTOs.EmployeeDTOs;
using RMS.Business.Exceptions;
using RMS.Business.Helpers.Email;
using RMS.Business.Helpers.FileHelpers;
using RMS.Business.Services.Abstracts;
using RMS.Core.Entities;
using System;
using System.IO;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailService _mailService;

        public EmployeeController(UserManager<AppUser> userManager,
           IWebHostEnvironment webHostEnvironment,
            RoleManager<IdentityRole> roleManager,
            IMailService mailService)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _roleManager = roleManager;
            _mailService = mailService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var employeeDtos = new List<EmployeeGetDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains("Admin") && !roles.Contains("Member"))
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

        public IActionResult RegisterEmployee()
        {
            ViewData["Roles"] = _roleManager.Roles.Select(r => r.Name).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmployee(RegisterEmployeeDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _roleManager.Roles.Select(r => r.Name).ToList();
                return View(registerDto);
            }
            if (registerDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image file not found!!");
                return View();
            }
            if (!registerDto.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Image file content type error!!");
                return View();
            }
            if (registerDto.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "Image file Size error!!");
                return View();
            }
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(registerDto.ImageFile.FileName);
            string path = _webHostEnvironment.WebRootPath + @"\uploads\team\" + filename;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                registerDto.ImageFile.CopyTo(stream);
            }
            
            AppUser user = new AppUser()
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Surname = registerDto.Surname,
                UserName = registerDto.UserName,
                ImageUrl = filename,
                Biography = registerDto.Biography
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                ViewData["Roles"] = _roleManager.Roles.Select(r => r.Name).ToList();
                return View(registerDto);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, HttpContext.Request.Scheme);
            await _mailService.SendEmailAsync(new MailRequest()
            {
                Subject = "Confirm Email",
                ToEmail = user.Email,
                Body = $"<a href='{link}'>Confirm Email</a>"
            });

            await _userManager.AddToRoleAsync(user, registerDto.Role);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Update(string id)
        {
            if (id == null) return View("error");

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return View("Error");

            var roles = await _userManager.GetRolesAsync(user);
            var model = new UpdateEmployeeDto
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                UserName = user.UserName,
                Role = roles.FirstOrDefault(),
                Biography = user.Biography,
                ImageFile = user.ImageFile
                
            };

            ViewData["Roles"] = _roleManager.Roles.Select(r => r.Name).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, UpdateEmployeeDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _roleManager.Roles.Select(r => r.Name).ToList();
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return View("Error");

            if (model.ImageFile != null)
            {
                if (!model.ImageFile.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("ImageFile", "Image file content type error!!");
                    return View();
                }
                if (model.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image file Size error!!");
                    return View();
                }
                    

                string oldpath = _webHostEnvironment.WebRootPath + @"\uploads\team\" + user.ImageUrl;
                try
                {
                    FileHelper.DeleteFile(oldpath);
                }catch(RMS.Business.Exceptions.FileNotFoundException ex)
                {
                    ModelState.AddModelError("ImageFile", ex.Message);
                    return View(model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ImageFile", "The file is being used by another process. Please try again later.");
                    return View(model);
                }
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                string path = _webHostEnvironment.WebRootPath + @"\uploads\team\" + filename;
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }

                user.ImageUrl = filename;
            }


            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.Biography = model.Biography;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                ViewData["Roles"] = _roleManager.Roles.Select(r => r.Name).ToList();
                return View(model);
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            if (!userRoles.Contains(model.Role))
            {
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return View("Error");

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return View("Error");

            try
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Error");
            }

            return RedirectToAction("Index");
        }
    }
}
