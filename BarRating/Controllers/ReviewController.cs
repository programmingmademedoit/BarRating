using BarRating.Data.Entities;
using BarRating.Models.Review;
using BarRating.Repository;
using BarRating.Service.Bar;
using BarRating.Service.Review;
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
                BarId = barId,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewViewModel model)
        {
            User loggedIn = await userManager.GetUserAsync(User);
            Review newReview = new Review()
            {
                
                Id = model.Id,
                BarId = model.BarId,
                Text = model.Text,
                CreatedBy = loggedIn,
                CreatedById = loggedIn.Id,
                CreatedOn = DateTime.Now,
            };
            await reviewService.Create(newReview, loggedIn);
            return RedirectToAction("Specify", "Bar", new {barId = model.BarId});
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Review review = reviewRepository.GetReviewById(id);
            EditReviewViewModel model = new EditReviewViewModel()
            {
                ReviewId = id,
                BarId = review.BarId,
                Text = review.Text,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditReviewViewModel model)
        {
            Review editedReview = reviewRepository.GetReviewById(model.ReviewId);
            
            editedReview.Text = model.Text;

            await reviewRepository.Edit(editedReview);
            return RedirectToAction("Specify", "Bar", new { barId = model.BarId });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Review review = reviewRepository.GetReviewById(id);
            DeleteReviewViewModel model = new DeleteReviewViewModel()
            {
                ReviewId = id,
                BarId = review.BarId,
                Text = review.Text,
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
        public async Task<IActionResult> ReviewsUser(int userId)
        {
            List<Data.Entities.Review> review = reviewRepository.GetAllUserReviews(userId);
            return View("UserReviews", review);
        }

        public IActionResult SearchReview(int id, string searchQuery)
        {
            ViewData["CurrentFilter"] = searchQuery;

            // Fetch the bar by ID
            var bar = barRepository.GetBarById(id);
            if (bar == null)
            {
                TempData["ErrorMessage"] = "Bar not found.";
                return RedirectToAction("Index", "Bar");
            }

            // Check if searchQuery is null or empty
            if (string.IsNullOrEmpty(searchQuery))
            {
                TempData["BarId"] = id;
                return RedirectToAction("NoResults", "Bar");
            }

            // Filter reviews based on searchQuery
            var filteredComments = bar.Reviews
                .Where(r => r.Text.StartsWith(searchQuery, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!filteredComments.Any())
            {
                TempData["BarId"] = id;
                return RedirectToAction("NoResults", "Bar");
            }

            // Create the view model
            var viewModel = new ReviewViewModel
            {
                BarId = bar.Id,
                Name = bar.Name,
                Description = bar.Description,
                Reviews = filteredComments.Select(r => reviewService.GetSpecifyPage(r)).ToList()
            };

            TempData["BarId"] = id;
            return View("~/Views/Bar/Specify.cshtml", viewModel);
        }


    }
}
