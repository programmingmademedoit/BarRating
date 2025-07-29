using BarRating.Data.Entities;
using BarRating.Repository;
using BarRating.Service.HelpfulVote;
using BarRating.Service.SavedBar;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BarRating.Controllers
{
    public class HelpfulVoteController : Controller
    {
        private readonly IHelpfulVoteService helpfulVoteService;
        private readonly ReviewRepository reviewRepository;
        private readonly HelpfulVoteRepository helpfulVoteRepository;
        private readonly UserRepository userRepository;
        private readonly UserManager<Data.Entities.User> userManager;
        public HelpfulVoteController(
            IHelpfulVoteService helpfulVoteService,
            ReviewRepository reviewRepository,
            HelpfulVoteRepository helpfulVoteRepository,
            UserRepository userRepository,
            UserManager<Data.Entities.User> userManager)
        {
            this.helpfulVoteService = helpfulVoteService;
            this.reviewRepository = reviewRepository;
            this.helpfulVoteRepository = helpfulVoteRepository;
            this.userRepository = userRepository;
            this.userManager = userManager;
        }
        public async Task<IActionResult> HelpfulVote(int reviewId)
        {
            Data.Entities.User loggedIn = await userManager.GetUserAsync(User);
            Data.Entities.Review review = reviewRepository.GetReviewById(reviewId);
            var isAlreadySaved = helpfulVoteService.HasUserVoted(reviewId, loggedIn.Id);
            if (isAlreadySaved)
            {
                await helpfulVoteService.Delete(reviewId, loggedIn.Id);
                return Json(new { success = true, isSaved = false, message = "Helpful vote removed." });
            }
            else
            {
                helpfulVoteService.Create(reviewId, loggedIn.Id);
                return Json(new { success = true, message = "Helpful vote added!" });
            }
        }
    }
}
