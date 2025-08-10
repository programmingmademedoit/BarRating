
using BarRating.Data.Entities;
using BarRating.Data.Enums;
using BarRating.Models.HelpfulVote;
using BarRating.Models.SavedBar;

namespace BarRating.Models.Review
{
    public class ReviewViewModel
    {
         public int ReviewId { get; set; }
        public int BarId { get; set; }
        public string BarName { get; set; }
        public string BarImage { get; set; }
        public int CreatedById { get; set; }

        public string Text { get; set; }
        public int Rating { get; set; }

        public int HelpfulCount { get; set; }
        public bool IsHelpfulByCurrentUser { get; set; } = false;
        public DateTime? EditedAt { get; set; }
        public DateTime CreatedOn { get; set; }
        public PriceCategory? Price { get; set; }
        public NumberOfPeople? NumberOfPeople { get; set; }
        public List<Tags>? Tags { get; set; }
        public string? OwnerReply { get; set; } 
        public int? OwnerId { get; set; }
        public DateTime? OwnerRepliedAt { get; set; }
        public DateTime? OwnerReplyEditedAt { get; set; }
        public string UserName { get; set; }
        public Rank UserRank { get; set; }
        public bool IsVerifiedUser { get; set; }
    }
}