using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Models.Bar;
using BarRating.Models.Review;
using BarRating.Repository;
using BarRating.Service.Bar;
using BarRating.Service.Photo;
using BarRating.Service.Review;
using BarRating.Service.SavedBar;
using BarRating.Service.Schedule;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace BarRating.Controllers
{
    public class BarController : Controller
    {
        private readonly BarRepository barRepository;
        private readonly IBarService barService;
        private readonly UserManager<User> userManager;
        private readonly IPhotoService photoService;
        private readonly IReviewService reviewService;
        private readonly UserRepository userRepository;
        private readonly ISavedBarService savedBarService;
        private readonly IScheduleService scheduleService;
        private readonly ApplicationDbContext context;
        public BarController(BarRepository barRepository,
            IBarService barService,
            UserManager<User> userManager,
            IPhotoService photoService,
            UserRepository userRepository,
            IReviewService reviewService,
            ISavedBarService savedBarService,
            IScheduleService scheduleService,
            ApplicationDbContext context)
        {
            this.barRepository = barRepository;
            this.barService = barService;
            this.userManager = userManager;
            this.photoService = photoService;
            this.userRepository = userRepository;
            this.reviewService = reviewService;
            this.savedBarService = savedBarService;
            this.scheduleService = scheduleService;
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Data.Entities.Bar> bars = barRepository.GetAllBars();
            if (User.Identity.IsAuthenticated)
            {
                User user = await userManager.GetUserAsync(User);

                var savedBarIds = context.SavedBars
                    .Where(sb => sb.CreatedById == user.Id)
                    .Select(sb => sb.BarId)
                    .ToHashSet();

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

                return View(model);
            }
            else
            {
                List<IndexViewModel> index = bars.Select(bar => new IndexViewModel
                {
                    BarId = bar.Id,
                    Name = bar.Name,
                    Description = bar.Description,
                    Image = bar.Image,
                    PriceCategory = bar.PriceCategory,
                    IsVerified = bar.IsVerified,
                    AverageRating = bar.Reviews.Any() ? bar.Reviews.Average(r => r.Rating) : 0,
                    ReviewsCount = bar.Reviews.Count,
                    IsSaved = false
                }).ToList();


                BarsViewModel model = new BarsViewModel
                {
                    Bars = index,
                };
                return View(model);
            }



        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateBarViewModel model = new CreateBarViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "You were unable to create a bar.";
                return View(model);
            }
            User loggedIn = await userManager.GetUserAsync(User);

            Bar bar = await barService.Create(model, loggedIn.Id);
            return RedirectToAction("Specify", "Bar", new { barid = bar.Id });

        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Edit(int barId)
        {
            EditBarViewModel model = await barService.GetEdit(barId);
            if(model == null)
            {
                TempData["Error"] = "You were unable to edit the bar.";
                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Edit(EditBarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                Bar bar = await barService.PostEdit(model);
                return RedirectToAction("Specify", "Bar", new { barId = model.BarId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred: " + ex.Message);
                return View(model);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Delete(int barId)
        {
            Bar bar = barRepository.GetBarById(barId);
            if (bar == null)
            {
                TempData["Error"] = "Bar was not found";
                return RedirectToAction("Index", "Bar");
            }
            DeleteBarViewModel model = new DeleteBarViewModel()
            {
                BarId = barId
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Delete(DeleteBarViewModel model)
        {
            Bar deletedBar = barRepository.GetBarById(model.BarId);
            if (deletedBar == null)
            {
                TempData["Error"] = "Bar couldn't be deleted. Please try again.";
                return View(model);
            }
            await barService.Delete(deletedBar);
            return RedirectToAction("Index", "Bar");
        }

        public async Task<IActionResult> Specify(int barId)
        {


            Data.Entities.Bar bar = barRepository.GetBarById(barId);
            if(bar == null)
            {
                TempData["Error"] = "The bar you were looking for couldnt't be found. PLease try again.";
                return RedirectToAction("Index", "Bar");
            }

            var priceCategories = bar.Reviews
                .Where(r => r.Price.HasValue)
                .Select(r => r.Price.Value)
                .ToList();

            if (priceCategories.Count >= 10)
            {
                var mostCommon = priceCategories
                    .GroupBy(c => c)
                    .OrderByDescending(g => g.Count())
                    .First();

                if (mostCommon.Count() >= 10)
                {
                    bar.PriceCategory = mostCommon.Key;
                }
            }
            int? userId = 0;
            if (User.Identity.IsAuthenticated)
            {
                User user = await userManager.GetUserAsync(User);
                userId = user.Id;
            }



            BarDetailViewModel model = new BarDetailViewModel()
            {
                BarId = barId,
                Name = bar.Name,
                Description = bar.Description,
                Location = bar.Location,
                BarType = bar.BarType,
                Website = bar.Website,
                PhoneNumber = bar.PhoneNumber,
                Instagram = bar.Instagram,
                Schedules = scheduleService.GetBarScheduleViewModel(bar.Schedules),
                ScheduleOverrides = scheduleService.GetBarScheduleOverrideViewModel(bar.ScheduleOverrides),
                HasLiveMusic = bar.HasLiveMusic,
                HasFood = bar.HasFood,
                HasOutdoorSeating = bar.HasOutdoorSeating,
                AcceptsReservations = bar.AcceptsReservations,
                HasParking = bar.HasParking,
                IsWheelchairAccessible = bar.IsWheelchairAccessible,
                PriceCategory = bar.PriceCategory,
                IsVerified = bar.IsVerified,
                OwnerId = bar.OwnerId,
                Image = bar.Image,
                IsSaved = savedBarService.IsSaved(bar.Id, userId.Value),
                Reviews = reviewService.GetSpecifyPage(bar.Reviews, userId.Value),
                AverageRating = bar.Reviews.Any() ? bar.Reviews.Average(r => r.Rating) : 0
            };

            return View(model);

        }
        /*public async Task<IActionResult> Search(string searchQuery)
        {
            ViewData["CurrentFilter"] = searchQuery;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                List<Bar> bars = barRepository.Search(searchQuery);
                List<IndexViewModel> index = bars.Select(bar => new IndexViewModel
                {
                    BarId = bar.Id,
                    Name = bar.Name,
                    Description = bar.Description,
                    Image = bar.Image
                }).ToList();

                BarViewModel model = new BarViewModel
                {
                    Bars = index,
                };
                return View("~/Views/Bar/Index.cshtml", model);
            }
            else
            {
                return RedirectToAction("Index", "Bar");
            }
        }
        public IActionResult NoResults()
        {
            if (TempData["BarId"] == null)
            {
                return RedirectToAction("Index", "Bar");
            }
            else
            {
                int id = (int)TempData["BarId"];
                Data.Entities.Bar bar = barRepository.GetBarById(id);
                ReviewViewModel model = new ReviewViewModel
                {
                    Name = bar.Name,
                    Description = bar.Description,
                    BarId = bar.Id,
                };
                return View(model);
            }
        }*/
        public async Task<IActionResult> GetUserSavedBars()
        {
            User loggedIn = await userManager.GetUserAsync(User);
            if (loggedIn == null)
            {
                TempData["Error"] = "An error ocurred. Please try again later.";
                return RedirectToAction("Index", "Bar");
            }
            List<Data.Entities.Bar> bars = barRepository.GetUserSavedBars(loggedIn.Id);
            if (bars == null)
            {
                TempData["Error"] = "An error ocurred. Please try again later.";
                return RedirectToAction("Index", "Bar");
            }
            var savedBarIds = context.SavedBars
                .Where(sb => sb.CreatedById == loggedIn.Id)
                .Select(sb => sb.BarId)
                .ToHashSet();

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
                    IsSaved = savedBarIds.Contains(bar.Id)
                }).ToList()
            };
            return View(model);

        }
    }
}
