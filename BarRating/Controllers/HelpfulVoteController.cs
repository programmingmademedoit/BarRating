using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Repository;
using BarRating.Service.HelpfulVote;
using BarRating.Service.Notification;
using BarRating.Service.SavedBar;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BarRating.Controllers
{
    public class HelpfulVoteController : Controller
    {
        private readonly IHelpfulVoteService helpfulVoteService;
        private readonly ReviewRepository reviewRepository;
        private readonly HelpfulVoteRepository helpfulVoteRepository;
        private readonly UserRepository userRepository;
        private readonly UserManager<Data.Entities.User> userManager;
        private readonly ApplicationDbContext context;
        private readonly INotificationService notificationService;
        public HelpfulVoteController(
            IHelpfulVoteService helpfulVoteService,
            ReviewRepository reviewRepository,
            HelpfulVoteRepository helpfulVoteRepository,
            UserRepository userRepository,
            UserManager<Data.Entities.User> userManager,
            ApplicationDbContext context,
            INotificationService notificationService)
        {
            this.helpfulVoteService = helpfulVoteService;
            this.reviewRepository = reviewRepository;
            this.helpfulVoteRepository = helpfulVoteRepository;
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.context = context;
            this.notificationService = notificationService;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HelpfulVote([FromBody] JsonElement body)
        {
            if (!body.TryGetProperty("reviewId", out var reviewIdElement) ||
                !reviewIdElement.TryGetInt32(out int reviewId))
            {
                return BadRequest(new { success = false, message = "Invalid reviewId." });
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var existing = helpfulVoteService.HasUserVoted(reviewId, user.Id);
            Review review = reviewRepository.GetReviewById(reviewId);
            if (existing)
            {
                await helpfulVoteService.Delete(reviewId, user.Id);
            }
            else
            {
                await helpfulVoteService.Create(reviewId, user.Id);
                await notificationService.Create($"{user.UserName} found your review helpful", review.CreatedById);
            }

            var newCount = context.HelpfulVotes.Count(v => v.ReviewId == reviewId);

            return Json(new
            {
                success = true,
                isSaved = !existing,
                count = newCount
            });
        }

        private int GetHelpfulCount(int reviewId)
        {
            return context.HelpfulVotes.Count(h => h.ReviewId == reviewId);
        }
    }
}
