using BarRating.Data.Entities;
using BarRating.Models.Reply;
using BarRating.Models.Review;
using BarRating.Repository;
using BarRating.Service.Bar;
using BarRating.Service.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BarRating.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly ReviewRepository reviewRepository;
        private readonly UserRepository userRepository;
        private readonly UserManager<User> userManager;
        private readonly BarRepository barRepository;
        private readonly IBarService barService;
        public ReviewController(IReviewService reviewService,
            ReviewRepository reviewRepository,
            UserRepository userRepository,
            UserManager<User> userManager,
            BarRepository barRepository
            , IBarService barService)
        {
            this.reviewService = reviewService;
            this.reviewRepository = reviewRepository;
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.barRepository = barRepository;
            this.barService = barService;
        }

        [HttpGet]
        public IActionResult Create(int barId)
        {

            CreateReviewViewModel model = new CreateReviewViewModel()
            { 
                BarId = barId 
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User loggedIn = await userManager.GetUserAsync(User);
            await reviewService.Create(model, loggedIn);
            return RedirectToAction("Specify", "Bar", new { barId = model.BarId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EditReviewViewModel model = await reviewService.GetEdit(id);
            var user = await userManager.GetUserAsync(User);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await reviewService.PostEdit(model);
                return RedirectToAction("Specify", "Bar", new { barId = model.BarId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the review.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Review review = reviewRepository.GetReviewById(id);
            DeleteReviewViewModel model = new DeleteReviewViewModel()
            {
                ReviewId = id,
                BarId = review.BarId,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteReviewViewModel model)
        {
            Review editedReview = reviewRepository.GetReviewById(model.ReviewId);
            await reviewRepository.Delete(editedReview);
            return RedirectToAction("Specify", "Bar", new { barId = model.BarId });
        }
        public async Task<IActionResult> UserReviews()
        {
            User loggedInUser = await userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                TempData["Error"] = "You need to log in to view your reviews.";
                return RedirectToAction("Login", "Account");
            }
            List<Data.Entities.Review> reviews = reviewRepository.GetAllUserReviews(loggedInUser.Id);
            return View(reviews);
        }


        /*public async Task<IActionResult> ReviewsUser(int userId)
        {
            List<Data.Entities.Review> review = reviewRepository.GetAllUserReviews(userId);
            return View("UserReviews", review);
        }

        public IActionResult SearchReview(int id, string searchQuery)
        {
            ViewData["CurrentFilter"] = searchQuery;

            var bar = barRepository.GetBarById(id);
            if (bar == null)
            {
                TempData["ErrorMessage"] = "Bar not found.";
                return RedirectToAction("Index", "Bar");
            }

            if (string.IsNullOrEmpty(searchQuery))
            {
                TempData["BarId"] = id;
                return RedirectToAction("NoResults", "Bar");
            }

            var filteredReviews = bar.Reviews
                .Where(r => r.Text.StartsWith(searchQuery, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!filteredReviews.Any())
            {
                TempData["BarId"] = id;
                return RedirectToAction("NoResults", "Bar");
            }

            var viewModel = new ReviewViewModel
            {
                BarId = bar.Id,
                Name = bar.Name,
                Description = bar.Description,
                Reviews = filteredReviews.Select(r => reviewService.GetSpecifyPage(r)).ToList()
            };

            TempData["BarId"] = id;
            return View("~/Views/Bar/Specify.cshtml", viewModel);
        }*/


    }

}
