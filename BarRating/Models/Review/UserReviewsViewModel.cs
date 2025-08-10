using BarRating.Data.Enums;
using BarRating.Models.SavedBar;

namespace BarRating.Models.Review
{
    public class UserReviewsViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Rank Rank { get; set; }
        public bool IsVerified { get; set; } = false;
        public ICollection<SavedBarViewModel> SavedBars { get; set; } = new List<SavedBarViewModel>();
        public ICollection<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
    }
}
