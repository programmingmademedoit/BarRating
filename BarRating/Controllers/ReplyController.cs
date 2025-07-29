using BarRating.Data.Entities;
using BarRating.Models.Reply;
using BarRating.Repository;
using BarRating.Service.Bar;
using BarRating.Service.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BarRating.Controllers
{
    public class ReplyController : Controller
    {
        private readonly BarRepository barRepository;
        private readonly UserRepository userRepository;
        private readonly UserManager<User> userManager;
        private readonly ReviewRepository reviewRepository;
        private readonly IBarService barService;
        private readonly IReviewService reviewService;
        public ReplyController(BarRepository barRepository,
            UserRepository userRepository,
            UserManager<User> userManager,
            ReviewRepository reviewRepository,
            IBarService barService,
            IReviewService reviewService)
        {
            this.barRepository = barRepository;
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.reviewRepository = reviewRepository;
            this.barService = barService;
            this.reviewService = reviewService;
        }
        [HttpGet]
        public async Task<IActionResult> OwnerReply(int reviewId)
        {
            Review review = reviewRepository.GetReviewById(reviewId);
            if (review == null)
            {
                TempData["ErrorMessage"] = "Review not found.";
                return RedirectToAction("Index", "Bar");
            }
            Bar bar = barRepository.GetBarById(review.BarId);
            if (bar == null)
            {
                TempData["ErrorMessage"] = "Bar not found.";
                return RedirectToAction("Index", "Bar");
            }
            User loggedIn = await userManager.GetUserAsync(User);
            if (bar.OwnerId != loggedIn.Id)
            {
                return Forbid();
            }
            ReplyViewModel model = new ReplyViewModel
            {
                ReviewId = review.Id,
                BarId = bar.Id,
                OwnerReply = review.OwnerReply ?? ""
            };
            return View(model);

        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OwnerReply(ReplyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Review review = reviewRepository.GetReviewById(model.ReviewId);
            Bar bar = barRepository.GetBarById(model.BarId);
            User loggedIn = await userManager.GetUserAsync(User);

            if (bar.OwnerId != loggedIn.Id)
            {
                return Forbid();
            }

            review.OwnerReply = model.OwnerReply;
            if (review.OwnerRepliedAt == null)
            {
                review.OwnerRepliedAt = DateTime.UtcNow;
                review.OwnerReplyEditedAt = null;
            }
            else
            {
                review.OwnerReplyEditedAt = DateTime.UtcNow;
            }
            await reviewRepository.Edit(review);
            return RedirectToAction("Specify", "Bar", new { barId = model.BarId });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteReply(int reviewId)
        {
            Review review = reviewRepository.GetReviewById(reviewId);
            if (review == null)
            {
                TempData["ErrorMessage"] = "Review not found.";
                return RedirectToAction("Index", "Bar");
            }
            Bar bar = barRepository.GetBarById(review.BarId);
            if (bar == null)
            {
                TempData["ErrorMessage"] = "Bar not found.";
                return RedirectToAction("Index", "Bar");
            }
            User loggedIn = await userManager.GetUserAsync(User);
            if (bar.OwnerId != loggedIn.Id)
            {
                return Unauthorized();
            }
            ReplyViewModel model = new ReplyViewModel
            {
                ReviewId = review.Id,
                BarId = bar.Id,
                OwnerReply = review.OwnerReply
            };
            return View("~/Views/Reply/DeleteReply.cshtml", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReply(ReplyViewModel model)
        {
            Review review = reviewRepository.GetReviewById(model.ReviewId);
            if (review == null)
            {
                TempData["ErrorMessage"] = "Review not found.";
                return RedirectToAction("Index", "Bar");
            }
            Bar bar = barRepository.GetBarById(review.BarId);
            if (bar == null)
            {
                TempData["ErrorMessage"] = "Bar not found.";
                return RedirectToAction("Index", "Bar");
            }
            User loggedIn = await userManager.GetUserAsync(User);
            if (bar.OwnerId != loggedIn.Id)
            {
                return Forbid();
            }
            review.OwnerReply = null;
            review.OwnerRepliedAt = null;
            review.OwnerReplyEditedAt = null;

            await reviewRepository.Edit(review);

            TempData["SuccessMessage"] = "Your reply was deleted successfully.";

            return RedirectToAction("Specify", "Bar", new { barId = model.BarId });
        }
    }
}
