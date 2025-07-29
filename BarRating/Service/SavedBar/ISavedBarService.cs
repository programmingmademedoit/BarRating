using BarRating.Models.SavedBar;

namespace BarRating.Service.SavedBar
{
    public interface ISavedBarService
    {
        public List<SavedBarViewModel> GetSavedBarsByUserId(int userId);
        public Task<Data.Entities.SavedBar> Create(int barId, int userId);
        public Task<Data.Entities.SavedBar> Delete(int barId, int userId);
        public bool IsSaved(int barId, int userId);
        public List<SavedBarViewModel> GetSavedBarViewModel(int barId);
    }
}