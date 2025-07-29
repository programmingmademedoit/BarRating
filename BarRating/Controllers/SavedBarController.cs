using BarRating.Data.Entities;
using BarRating.Repository;
using BarRating.Service.SavedBar;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BarRating.Controllers
{
    public class SavedBarController : Controller
    {
        private readonly SavedBarRepository savedBarRepository;
        private readonly ISavedBarService savedBarService;
        private readonly UserRepository userRepository;
        private readonly UserManager<Data.Entities.User> userManager;

        public SavedBarController (SavedBarRepository savedBarRepository,
            ISavedBarService savedBarService,
            UserRepository userRepository,
            UserManager<Data.Entities.User> userManager)
        {
            this.savedBarRepository = savedBarRepository;
            this.savedBarService = savedBarService;
            this.userRepository = userRepository;
            this.userManager = userManager;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavedBar(int barId)
        {
            Data.Entities.User loggedIn = await userManager.GetUserAsync(User);
            if (loggedIn == null)
            {
                return Json(new { success = false, message = "You must be logged in to save a bar." });
            }
            var isAlreadySaved = savedBarService.IsSaved(barId, loggedIn.Id);
            if (isAlreadySaved)
            {
                await savedBarService.Delete(barId, loggedIn.Id);
                return Json(new { success = true, isSaved = false, message = "Bar removed from saved." });
            }
            else
            {
                savedBarService.Create(barId, loggedIn.Id);
                return Json(new { success = true, message = "Bar saved!" });
            }
        }
    }
}
