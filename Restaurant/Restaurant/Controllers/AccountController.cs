using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMS.Business.DTOs.Account;
using RMS.Business.Helpers.Account;
using RMS.Business.Helpers.Email;
using RMS.Business.Services.Abstracts;
using RMS.Core.Entities;

namespace Restaurant.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailService _mailService;
        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mailService = mailService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = new AppUser()
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Surname = registerDto.Surname,
                UserName = registerDto.UserName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, HttpContext.Request.Scheme);
            await _mailService.SendEmailAsync(new MailRequest()
            {
                Subject = "Confirm Email",
                ToEmail = user.Email,
                Body = $"<a href='{link}'>Confirm Email</a>"
            });
            await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRole)))
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = item.ToString()
                });
            }

            return Ok();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.FindByNameAsync(loginDto.EmailOrUserName);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginDto.EmailOrUserName);
                if (user is null)
                {
                    ModelState.AddModelError("", "UsernameOrEmail ve ya password duzgun daxil edilmeyib");
                    return View();
                }
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Email təsdiqlənməyib. Zəhmət olmasa emailinizi təsdiqləyin.");
                return View();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Birazdan yeniden cehd edin!");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UsernameOrEmail ve ya password duzgun daxil edilmeyib");
                return View();
            }

            await _signInManager.SignInAsync(user, loginDto.IsRemember);
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordVm)
        {
            if (!ModelState.IsValid) return View();
            var user = await _userManager.FindByEmailAsync(forgotPasswordVm.Email);
            if (user is null || !(await _userManager.IsEmailConfirmedAsync(user))) {
                return View("ForgotPasswordConfirmation");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new { userId = user.Id, token = token }, HttpContext.Request.Scheme);
            await _mailService.SendEmailAsync(new MailRequest()
            {
                Subject = "Reset Password",
                ToEmail = user.Email,
                Body = $"Zəhmət olmasa parolu yeniləmək üçün linkə daxil olun: <a href='{link}'>Reset password</a>"
            });
            return RedirectToAction("ForgotPasswordConfirmation");
        }
        public async Task<IActionResult> ResetPassword(string userId, string token = null)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return View("Error");
            if (token is null) return View("Error");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto vm, string userId, string token)
        {
            if (!ModelState.IsValid) return View();
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return View("Error");
            }
            
            var result = await _userManager.ResetPasswordAsync(user, token, vm.Password);
            if (result.Succeeded)
            {
               return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return RedirectToAction("Login");
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
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

    }
}
