using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Models;
using BarRating.Models.User;
using BarRating.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BarRating.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserRepository user;
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger,
            UserRepository user,
            UserManager<User> userManager,
            ApplicationDbContext context)
        {
            _logger = logger;
            this.user = user;
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Users()
        {
            var users = await user.GetAllUsers();
            List<UserViewModel> usersList = new List<UserViewModel>();
            foreach (var user in users)
            {
                var model = new UserViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,

                };
                usersList.Add(model);
            }
            return View(usersList);
        }
        public IActionResult Dashboard()
        {
            var model = new DashboardViewModel
            {
                UserCount = userManager.Users.Count(),
                RestaurantCount = context.Bars.Count(),
                ReviewCount = context.Reviews.Count()
            };

            return View(model);
        }
    }
}