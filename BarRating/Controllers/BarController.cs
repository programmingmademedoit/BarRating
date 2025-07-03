using BarRating.Data.Entities;
using BarRating.Models.Bar;
using BarRating.Models.Review;
using BarRating.Repository;
using BarRating.Service.Bar;
using BarRating.Service.Photo;
using BarRating.Service.Review;
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
            List<Data.Entities.Bar> bars = barRepository.GetAllBars();
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
           
            return View(model);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            CreateBarViewModel model = new CreateBarViewModel()
            { 
                BarId = id
            };

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

            var result = await photoService.AddPhotoAsync(model.Image);

            Data.Entities.Bar bar = new Data.Entities.Bar()
            {
                Id = model.BarId,
                Name = model.Name,
                Description = model.Description,
                Image = result.Url.ToString(),
                CreatedBy = loggedIn,
                CreatedById = loggedIn.Id,
                CreatedOn = DateTime.Now,
            };
            await barRepository.Create(bar);
            return RedirectToAction("Index", "Bar", new { id = model.BarId });

        }
        [HttpGet]
        public IActionResult Edit(int barId)
        {
            Bar model = barRepository.GetBarById(barId);
            var bar = new EditBarViewModel
            {
                BarId = barId,
                Name = model.Name,
                Description = model.Description,
                URL = model.Image,
                CreatedById = model.CreatedById
            };
            return View(bar);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBarViewModel model)
        {
            Bar existingBar = barRepository.GetBarById(model.BarId);
            if (existingBar == null)
            {
                return View("Error");
            }

            string newImageUrl = existingBar.Image;
            if (model.Image != null)
            {
                var photoResult = await photoService.AddPhotoAsync(model.Image);
                if (photoResult.Error != null)
                {
                    ModelState.AddModelError("Image", "Photo upload failed");
                    return View(model);
                }

                if (!string.IsNullOrEmpty(existingBar.Image))
                {
                    await photoService.DeletePhotoAsync(existingBar.Image);
                }

                newImageUrl = photoResult.Url.ToString();
            }

            existingBar.Name = model.Name;
            existingBar.Description = model.Description;
            existingBar.Image = newImageUrl;
            existingBar.CreatedById = model.CreatedById;
            existingBar.CreatedBy = userRepository.GetUserById(existingBar.CreatedById);

            await barRepository.Edit(existingBar);

            return RedirectToAction("Index", "Bar", new { id = model.BarId });
        }
        [HttpGet]
        public IActionResult Delete(int barId)
        {
            Bar bar = barRepository.GetBarById(barId);
            DeleteBarViewModel model = new DeleteBarViewModel()
            {
                BarId = barId,
                Name = bar.Name,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteBarViewModel model)
        {
            Bar deletedBar = barRepository.GetBarById(model.BarId);
            deletedBar.Id = model.BarId;
            deletedBar.Name = model.Name;
            await barRepository.Delete(deletedBar);
            return RedirectToAction("Index", "Bar");
        }

        public IActionResult Specify(int barId)
        {
            Bar bar = barRepository.GetBarById(barId);
            ReviewViewModel model = new ReviewViewModel()
            {
                BarId = barId,
                Name = bar.Name,
                Description = bar.Description,
                Reviews = bar.Reviews.Select(r => reviewService.GetSpecifyPage(r)) 
            };
            return View(model);

        }
        public async Task<IActionResult> Search(string searchQuery)
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
        }

    }
}
