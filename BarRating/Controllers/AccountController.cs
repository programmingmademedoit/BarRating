using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Models;
using BarRating.Models.Account;
using BarRating.Models.User;
using BarRating.Service.Notification;
using BarRating.Service.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace BarRating.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly INotificationService notificationService;

        public AccountController(UserManager<User> userManager,
            IUserService userService,
            SignInManager<User> signInManager,
            ApplicationDbContext context,
            INotificationService notificationService 
            )
        {           
            this.userManager = userManager;
            this.userService = userService;
            _signInManager = signInManager;
            _context = context;
            this.notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginViewModel.Username);


                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    await notificationService.Create("Welcome the the best Bar Rating site", user.Id);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Wrong credentials. Please try again";
                    return View(loginViewModel);
                }

            }
            TempData["Error"] = "Wrong credentials. Please try again";
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var userWithEmail = await userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (userWithEmail != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }

            var userWithUsername = await userManager.FindByNameAsync(registerViewModel.Username);
            if (userWithUsername != null)
            {
                TempData["Error"] = "This username is already in use";
                return View(registerViewModel);
            }

            var newUser = new User()
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.Username
            };
            var newUserResponse = await userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, "User");
                return RedirectToAction("Login", "Account");
            }
            return View(registerViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Index()
        {
            User loggedIn = await userManager.GetUserAsync(User);
            AccountIndexViewModel model = await userService.Index(loggedIn.Id);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            User user = await userManager.GetUserAsync(User);
            AccountEditViewModel model = await userService.EditProfile(user.Id);
            return View("Edit",model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateName([FromBody] UpdateTwoFieldModels model)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            user.FirstName = model.ValueOne?.Trim();
            user.LastName = model.ValueTwo?.Trim();
            await userManager.UpdateAsync(user);

            return Json(new { success = true, firstName = user.FirstName, lastName = user.LastName });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserName([FromBody] UpdateFieldModel model)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            if (string.IsNullOrWhiteSpace(model.Value))
            {
                return Json(new { success = false, message = "Username cannot be empty." });
            }

            var trimmed = model.Value.Trim();

            var existingUser = await userManager.FindByNameAsync(trimmed);
            if (existingUser != null && existingUser.Id != user.Id)
            {
                return Json(new { success = false, message = "This username is already taken." });
            }

            user.UserName = trimmed;
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Json(new { success = true, value = user.UserName });
            }

            return Json(new
            {
                success = false,
                message = string.Join(", ", result.Errors.Select(e => e.Description))
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateFieldModel model)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var newEmail = model.Value?.Trim();
            if (string.IsNullOrEmpty(newEmail) || !IsValidEmail(newEmail))
                return Json(new { success = false, message = "Invalid email." });

            var token = await userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            user.Email = newEmail;
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Json(new { success = true, value = user.Email });

            return Json(new { success = false, message = string.Join(", ", result.Errors.Select(e => e.Description)) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateTwoFieldModels model)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var result = await userManager.ChangePasswordAsync(user, model.ValueOne, model.ValueTwo);

            if (result.Succeeded)
                return Json(new { success = true, message = "Password updated." });

            return Json(new { success = false, message = string.Join(", ", result.Errors.Select(e => e.Description)) });
        }

        private bool IsValidEmail(string email)
        {
            try { var addr = new System.Net.Mail.MailAddress(email); return addr.Address == email; }
            catch { return false; }
        }
    }
}