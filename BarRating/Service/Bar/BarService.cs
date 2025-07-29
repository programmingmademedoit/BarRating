using BarRating.Data.Entities;
using BarRating.Models.Bar;
using BarRating.Repository;
using BarRating.Service.Photo;
using BarRating.Service.Review;
using BarRating.Service.SavedBar;
using BarRating.Service.Schedule;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
namespace BarRating.Service.Bar
{
    public class BarService : IBarService   
    {
        private readonly BarRepository repository;
        private readonly IPhotoService photoService;
        private readonly UserRepository userRepository;
        private readonly UserManager<Data.Entities.User> userManager;
        private readonly IReviewService reviewService;
        private readonly IScheduleService scheduleService;
        private readonly ISavedBarService savedBarService;
        public BarService(
            BarRepository repository,
            IPhotoService photoService,
            UserRepository userRepository,
            UserManager<Data.Entities.User> userManager,
            IReviewService reviewService,
            IScheduleService scheduleService,
            ISavedBarService savedBarService)
        {
            this.repository = repository;
            this.photoService = photoService;
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.reviewService = reviewService;
            this.scheduleService = scheduleService;
            this.savedBarService = savedBarService;
        }

        public async Task<Data.Entities.Bar> Create(CreateBarViewModel model, Data.Entities.User createdBy)
        {
            var result = await photoService.AddPhotoAsync(model.Image);

            Data.Entities.Bar bar = new Data.Entities.Bar
            {
                Name = model.Name,
                Description = model.Description,
                Location = model.Location,
                BarType = model.BarType,
                Website = model.Website,
                PhoneNumber = model.PhoneNumber,
                Instagram = model.Instagram,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Schedules = scheduleService.PostBarScheduleViewModel(model.Schedules),
                ScheduleOverrides = scheduleService.PostBarScheduleOverrideViewModel(model.ScheduleOverrides),
                HasLiveMusic = model.HasLiveMusic,
                HasFood = model.HasFood,
                HasOutdoorSeating = model.HasOutdoorSeating,
                AcceptsReservations = model.AcceptsReservations,
                HasParking = model.HasParking,
                IsWheelchairAccessible = model.IsWheelchairAccessible,
                IsVerified = model.IsVerified,
                Image = result.Url.ToString(),
                CreatedBy = createdBy,
                CreatedById = createdBy.Id,
                CreatedOn = model.CreatedOn

            };
                return await repository.Create(bar);



        }
        public async Task<EditBarViewModel> GetEdit(int barId)
        {
            Data.Entities.Bar bar = repository.GetBarById(barId);
            EditBarViewModel model = new EditBarViewModel
            {
                BarId = bar.Id,
                Name = bar.Name,
                Description = bar.Description,
                URL = bar.Image,
                Location = bar.Location,
                BarType = bar.BarType,
                Website = bar.Website,
                PhoneNumber = bar.PhoneNumber,
                Instagram = bar.Instagram,
                Latitude = bar.Latitude,
                Longitude = bar.Longitude,
                Schedules = scheduleService.GetBarScheduleViewModel(bar.Schedules),
                ScheduleOverrides = scheduleService.GetBarScheduleOverrideViewModel(bar.ScheduleOverrides),
                HasLiveMusic = bar.HasLiveMusic,
                HasFood = bar.HasFood,
                HasOutdoorSeating = bar.HasOutdoorSeating,
                AcceptsReservations = bar.AcceptsReservations,
                HasParking = bar.HasParking,
                IsWheelchairAccessible = bar.IsWheelchairAccessible,
                IsVerified = bar.IsVerified
            };
            return model;
        }
        public async Task<Data.Entities.Bar> PostEdit(EditBarViewModel model)
        {
            Data.Entities.Bar existingBar = repository.GetBarById(model.BarId);

            string newImageUrl = existingBar.Image;
            if (model.Image != null)
            {
                var photoResult = await photoService.AddPhotoAsync(model.Image);
                if (photoResult.Error != null)
                {
                    throw new Exception("Image upload failed: " + photoResult.Error.Message);
                }

                if (!string.IsNullOrEmpty(existingBar.Image))
                {
                    await photoService.DeletePhotoAsync(existingBar.Image);
                }

                newImageUrl = photoResult.Url.ToString();
            }
            existingBar.Image = newImageUrl;

            existingBar.Name = model.Name;
            existingBar.Description = model.Description;
            existingBar.Location = model.Location;
            existingBar.BarType = model.BarType;
            existingBar.Website = model.Website;
            existingBar.PhoneNumber = model.PhoneNumber;
            existingBar.Instagram = model.Instagram;
            existingBar.Latitude = model.Latitude;
            existingBar.Longitude = model.Longitude;
            existingBar.Schedules = scheduleService.PostBarScheduleViewModel(model.Schedules);
            existingBar.ScheduleOverrides = scheduleService.PostBarScheduleOverrideViewModel(model.ScheduleOverrides);
            existingBar.HasLiveMusic = model.HasLiveMusic;
            existingBar.HasFood = model.HasFood;
            existingBar.HasOutdoorSeating = model.HasOutdoorSeating;
            existingBar.AcceptsReservations = model.AcceptsReservations;
            existingBar.HasParking = model.HasParking;
            existingBar.IsWheelchairAccessible = model.IsWheelchairAccessible;
            existingBar.IsVerified = model.IsVerified;

            return await repository.Edit(existingBar);
        }

        public async Task<Data.Entities.Bar> Delete(Data.Entities.Bar bar)
        {
            return await repository.Delete(bar);
        }

        public BarDetailViewModel Specify (int barId, int userId)
        {
            Data.Entities.Bar bar = repository.GetBarById(barId);

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
                SavedBars = savedBarService.GetSavedBarViewModel(barId),
                IsSaved = savedBarService.IsSaved(bar.Id, userId),
                Reviews = reviewService.GetSpecifyPage(bar.Reviews, userId),
                AverageRating = bar.Reviews.Any() ? bar.Reviews.Average(r => r.Rating) : 0
            };
            return model;
        }
        public BarsViewModel Index()
        {
            List<Data.Entities.Bar> bars = repository.GetAllBars(); 
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
               // IsSaved = savedBarService.IsSaved(bar.Id, userId),
            }).ToList();

            BarsViewModel model = new BarsViewModel
            { 
                Bars = index,
            };
            return model;
        }
        public BarsViewModel OwnerBars(int userId)
        {
            List<Data.Entities.Bar> bars = repository
                .GetAllBars()
                .Where(b => b.OwnerId == userId && b.Owner.IsVerified == true &&
                b.IsVerified == true)
                .ToList();
            List<IndexViewModel> index = bars.Select(bar => new IndexViewModel
            {
                BarId = bar.Id,
                Name = bar.Name,
                Description = bar.Description,
                Image = bar.Image,
                PriceCategory = bar.PriceCategory,
                IsVerified = bar.IsVerified,
                AverageRating = bar.Reviews.Any() ? bar.Reviews.Average(r => r.Rating) : 0
            }).ToList();

            BarsViewModel model = new BarsViewModel
            {
                Bars = index,
            };
            return model;
        }
    }
}
