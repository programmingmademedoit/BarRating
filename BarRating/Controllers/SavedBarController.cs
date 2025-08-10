using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Models.Bar;
using BarRating.Models.SavedBar;
using BarRating.Repository;
using BarRating.Service.SavedBar;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BarRating.Controllers
{
    public class SavedBarController : Controller
    {
        private readonly SavedBarRepository savedBarRepository;
        private readonly ISavedBarService savedBarService;
        private readonly UserRepository userRepository;
        private readonly UserManager<Data.Entities.User> userManager;
        private readonly ApplicationDbContext context;
        private readonly BarRepository barRepository;

        public SavedBarController (SavedBarRepository savedBarRepository,
            ISavedBarService savedBarService,
            UserRepository userRepository,
            UserManager<Data.Entities.User> userManager,
            ApplicationDbContext context,
            BarRepository barRepository)
        {
            this.savedBarRepository = savedBarRepository;
            this.savedBarService = savedBarService;
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.context = context;
            this.barRepository = barRepository;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavedBar([FromBody] JsonElement body)
        {
            if (!body.TryGetProperty("barId", out var barIdElement))
                return BadRequest(new { success = false, message = "barId is required." });

            if (!barIdElement.TryGetInt32(out int barId) || barId <= 0)
                return BadRequest(new { success = false, message = "Invalid barId." });

            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var isAlreadySaved = savedBarService.IsSaved(barId, user.Id);

            if (isAlreadySaved)
            {
                await savedBarService.Delete(barId, user.Id);
                return Json(new { success = true, isSaved = false, message = "Bar removed from saved." });
            }
            else
            {
                await savedBarService.Create(barId, user.Id);
                return Json(new { success = true, isSaved = true, message = "Bar saved!" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> UserSavedBars()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            BarsViewModel model = await savedBarService.GetUserSavedBars(user.Id);

            return PartialView("UserSavedBars", model);
        }
        /*public async Task<IActionResult> UserSavedBars()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var savedBarIds = context.SavedBars
                    .Where(sb => sb.CreatedById == user.Id)
                    .Select(sb => sb.BarId)
                    .ToHashSet();

            List<Bar> bars = barRepository.GetUserSavedBars(user.Id);
            var model = new BarsViewModel
            {
                Bars = bars.Select(bar => new IndexViewModel
                {
                    BarId = bar.Id,
                    Name = bar.Name,
                    Description = bar.Description,
                    Image = bar.Image,
                    PriceCategory = bar.PriceCategory,
                    IsVerified = bar.IsVerified,
                    AverageRating = bar.Reviews.Any() ? bar.Reviews.Average(r => r.Rating) : 0,
                    ReviewsCount = bar.Reviews.Count,
                    IsSaved = savedBarIds.Contains(bar.Id) // ✅ O(1) check
                }).ToList()
            };

            return PartialView("UserSavedBars", model);
        }*/
    }
}
