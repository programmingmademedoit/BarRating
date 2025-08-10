using BarRating.Data.Enums;
using BarRating.Models.Review;
using BarRating.Models.SavedBar;

namespace BarRating.Models.Account
{
    public class AccountIndexViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public Rank Rank { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public bool IsVerified { get; set; } = false;
    }
}
