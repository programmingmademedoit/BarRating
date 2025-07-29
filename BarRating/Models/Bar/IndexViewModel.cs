
using BarRating.Data.Enums;
using BarRating.Models.SavedBar;

namespace BarRating.Models.Bar
{
    public class IndexViewModel
    {
        public int BarId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public BarType BarType { get; set; }
        public PriceCategory PriceCategory { get; set; }
        public double AverageRating { get; set; }
        public int ReviewsCount { get; set; }
        public bool IsVerified { get; set; }
        public ICollection<SavedBarViewModel> SavedBars { get; set; }
        public bool IsSaved { get; set; }
    }
}