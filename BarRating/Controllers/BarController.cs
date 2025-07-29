using BarRating.Data.Entities;
using BarRating.Models.Bar;
using BarRating.Models.Review;
using BarRating.Repository;
using BarRating.Service.Bar;
using BarRating.Service.Photo;
using BarRating.Service.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public BarController(BarRepository barRepository,
            IBarService barService,
            UserManager<User> userManager,
            IPhotoService photoService,
            UserRepository userRepository,
            IReviewService reviewService)
        {
            this.barRepository = barRepository;
            this.barService = barService;
            this.userManager = userManager;
            this.photoService = photoService;
            this.userRepository = userRepository;
            this.reviewService = reviewService;
        }
        public IActionResult Index()
        {

            BarsViewModel model = barService.Index();
            return View(model);
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
            if(!ModelState.IsValid)
            {
                
                return View(model);
            }
            User loggedIn = await userManager.GetUserAsync(User);

            Bar bar = await barService.Create(model, loggedIn);
            return RedirectToAction("Specify", "Bar", new { barid = bar.Id });

        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Edit(int barId)
        {
            EditBarViewModel model = await barService.GetEdit(barId);
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
            await barService.Delete(deletedBar);
            return RedirectToAction("Index", "Bar");
        }

        public async Task<IActionResult> Specify(int barId)
        {
            Data.Entities.User user = await userManager.GetUserAsync(User);
            BarDetailViewModel model = barService.Specify(barId, user.Id);
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

    }
}
