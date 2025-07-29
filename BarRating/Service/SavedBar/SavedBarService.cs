using BarRating.Data.Entities;
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

        public List<SavedBarViewModel> GetSavedBarsByUserId(int userId)
        {
            List<Data.Entities.SavedBar> savedBars = savedBarRepository.GetUserSavedBars(userId);
            return savedBars.Select(sb => new Models.SavedBar.SavedBarViewModel
            {
                Id = sb.Id,
                BarId = sb.BarId,
                UserId = userId,
                CreatedOn = sb.CreatedOn
            }).ToList();
        }
        public async Task<Data.Entities.SavedBar> Create(int barId, int userId)
        {
            Data.Entities.SavedBar savedBar = new Data.Entities.SavedBar
            {
                BarId = barId,
                CreatedById = userId,
                CreatedOn = DateTime.UtcNow
            };
            return await savedBarRepository.Create(savedBar);
        }
        public async Task<Data.Entities.SavedBar> Delete(int barId, int userId)
        {
            Data.Entities.SavedBar savedBar = savedBarRepository.GetSavedBarByBarIdandUserId(barId, userId);
            return await savedBarRepository.Delete(savedBar);
        }
        public bool IsSaved(int barId, int userId)
        {
            var savedBar = savedBarRepository.GetSavedBarByBarIdandUserId(barId, userId);
            return savedBar != null;
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
    }
}
