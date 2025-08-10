using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Models;
using BarRating.Models.Account;
using BarRating.Models.Admin;
using BarRating.Models.Review;
using BarRating.Models.User;
using BarRating.Repository;
using BarRating.Service.Review;
using BarRating.Service.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BarRating.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository userRepository;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly ApplicationDbContext context;
        public UserController(UserRepository userRepository,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ApplicationDbContext context,
            IUserService userService)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
            this.userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(int userId)
        {
            EditUserRoleViewModel model = await userService.EditRole(userId);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditRole(EditUserRoleViewModel model)
        {
            var user = userRepository.GetUserById(model.UserId);    
            if (user == null)
            {
                return NotFound();
            }

            // Fetch all roles again to map role IDs to role names
            var allRoles = roleManager.Roles.ToList();
            var currentRoleNames = await userManager.GetRolesAsync(user);

            // Map selected role IDs to names
            var selectedRoleNames = allRoles
                .Where(r => model.SelectedRoles.Contains(r.Id))
                .Select(r => r.Name)
                .ToList();

            // Calculate roles to add and remove
            var rolesToAdd = selectedRoleNames.Except(currentRoleNames).ToList();
            var rolesToRemove = currentRoleNames.Except(selectedRoleNames).ToList();

            await userManager.AddToRolesAsync(user, rolesToAdd);
            await userManager.RemoveFromRolesAsync(user, rolesToRemove);

            return RedirectToAction("Users", "Home");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            Data.Entities.User user = new Data.Entities.User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,

            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Users", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int userId)
        {
            var user = userRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = userRepository.GetUserById(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Users", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int userId)
        {
            // Find the user by ID
            var user = userRepository.GetUserById(userId);
            if (user == null)
            {
                // If the user does not exist, return a not found result
                return NotFound();
            }

            // Delete the user
            var result = await userManager.DeleteAsync(user);

            // Check if the deletion was successful
            if (result.Succeeded)
            {
                return RedirectToAction("Users", "Home");
            }

            // If there was an error, add the errors to the model state and return to the same view
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            // Optionally, return to a view that displays the errors
            return View("Error"); // You might wa
        }

        /*[HttpGet]
        public async Task<IActionResult> EditFirstandLastName()
        {
            User loggedIn = await userManager.GetUserAsync(User);
            AccountEditViewModel model = await userService.EditFirstAndLastName(loggedIn.Id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditFirstandLastName(AccountEditViewModel model)
        {
            Data.Entities.User user = userRepository.GetUserById(model.UserId);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            var result = await userManager.UpdateAsync(user);
            return RedirectToAction("Account", "EditProfile", new { userId = model.UserId });
        }

        [HttpGet]
        public async Task<IActionResult> EditUserName()
        {
            User loggedIn = await userManager.GetUserAsync(User);
            AccountEditViewModel model = await userService.EditFirstAndLastName(loggedIn.Id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserName(AccountEditViewModel model)
        {
            Data.Entities.User user = userRepository.GetUserById(model.UserId);
            user.UserName = model.UserName;
            var userWithUsername = await userManager.FindByNameAsync(model.UserName);
            if (userWithUsername != null)
            {
                TempData["Error"] = "This Username is already in use";
                return View(model);
            }
            var result = await userManager.UpdateAsync(user);
            return RedirectToAction("Account", "EditProfile", new { userId = model.UserId });
        }

        [HttpGet]
        public async Task<IActionResult> EditEmail()
        {
            User loggedIn = await userManager.GetUserAsync(User);
            AccountEditViewModel model = await userService.EditFirstAndLastName(loggedIn.Id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmail(AccountEditViewModel model)
        {
            Data.Entities.User user = userRepository.GetUserById(model.UserId);
            user.Email = model.Email;
            var userWithEmail = await userManager.FindByEmailAsync(model.Email);
            if (userWithEmail != null)
            {
                TempData["Error"] = "This Email is already in use";
                return View(model);
            }
            var result = await userManager.UpdateAsync(user);
            return RedirectToAction("Account", "EditProfile", new { userId = model.UserId });
        }

        [HttpGet]
        public async Task<IActionResult> EditPassword()
        {
            User loggedIn = await userManager.GetUserAsync(User);
            AccountEditViewModel model = await userService.EditFirstAndLastName(loggedIn.Id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPassword(AccountEditViewModel model, string newPassword)
        {
            Data.Entities.User user = await userManager.GetUserAsync(User);
            var userWithPassword = await userManager.ChangePasswordAsync(user, model.Password, newPassword);
            if (model.Password == newPassword)
            {
                TempData["Error"] = "New password cannot be the same as the old password";
                return View(model);
            }
            var result = await userManager.UpdateAsync(user);
            return RedirectToAction("Account", "EditProfile", new { userId = model.UserId });
        }*/
        
    }
}
