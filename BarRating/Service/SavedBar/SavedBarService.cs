using BarRating.Data.Entities;
using BarRating.Models.Bar;
using BarRating.Models.SavedBar;
using BarRating.Repository;
using Microsoft.AspNetCore.Identity;

namespace BarRating.Service.SavedBar
{
    public class SavedBarService : ISavedBarService
    {
        private readonly SavedBarRepository savedBarRepository;
        private readonly UserRepository userRepository;
        private readonly UserManager<Data.Entities.User> userManager;
        private readonly BarRepository barRepository;

        public SavedBarService(
            SavedBarRepository savedBarRepository,
            UserRepository userRepository,
            UserManager<Data.Entities.User> userManager,
            BarRepository barRepository)
        {
            this.savedBarRepository = savedBarRepository;
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.barRepository = barRepository;
        }

        public List<IndexViewModel> GetSavedBarsByUserId(int userId)
        {
            List<Data.Entities.SavedBar> savedBars = savedBarRepository.GetUserSavedBars(userId);
             return savedBars.Select(sb => new Models.Bar.IndexViewModel
            {
            BarId = sb.BarId,
            BarType = sb.Bar.BarType,
            }).ToList();

        }
        public async Task<Data.Entities.SavedBar> Create(int barId, int userId)
        {
            Data.Entities.SavedBar savedBar = new Data.Entities.SavedBar
            {
                BarId = barId,
                Bar = barRepository.GetBarById(barId),
                CreatedBy  = userRepository.GetUserById(userId),
                CreatedById = userId,
                CreatedOn = DateTime.UtcNow
            };
            return await savedBarRepository.Create(savedBar);
        }
        public async Task<Data.Entities.SavedBar> Delete(int barId, int userId)
        {
            Data.Entities.SavedBar savedBar = savedBarRepository.GetSavedBarByBarIdandUserId(barId, userId);
            if(savedBar == null)
            {
                return null;
            }
            return await savedBarRepository.Delete(savedBar);
        }
        public bool IsSaved(int barId, int userId)
        {
            if(userId <= 0)
            {
                return false;
            }
            var savedBar = savedBarRepository.GetSavedBarByBarIdandUserId(barId, userId);
            if(savedBar == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public List<SavedBarViewModel> GetSavedBarViewModel(int barId)
        {
            List<Data.Entities.SavedBar> savedBars = savedBarRepository.GetSavedBarsByBarId(barId);

            return savedBars.Select( s => new SavedBarViewModel
            { 
                BarId = s.BarId,
                UserId = s.CreatedById,
                CreatedOn = s.CreatedOn,
            }).ToList();

        }
        public async Task<BarsViewModel> GetUserSavedBars(int userId)
        {
            List<Data.Entities.Bar> savedBars = barRepository.GetUserSavedBars(userId);
            List<IndexViewModel> viewModel = savedBars.Select( savedBar => new IndexViewModel
            {
                BarId = savedBar.Id,
                Name = savedBar.Name,
                Description = savedBar.Description,
                Image = savedBar.Image,
                PriceCategory = savedBar.PriceCategory,
                IsVerified = savedBar.IsVerified,
                AverageRating = savedBar.Reviews.Any() ? savedBar.Reviews.Average(r => r.Rating) : 0,
                ReviewsCount = savedBar.Reviews.Count,
                IsSaved = true
            }).ToList();

            BarsViewModel model = new BarsViewModel()
            { 
                Bars = viewModel
            };

            return model;

        }

    }
}
