
using BarRating.Data.Enums;

namespace BarRating.Data.Entities
{
    public class Review : MetadataBaseEntity
    {
        public int BarId { get; set; }
        public Bar Bar { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        public List<HelpfulVote> HelpfulVotes { get; set; }
        public DateTime? EditedAt { get; set; }
        public PriceCategory? Price { get; set; }
        public NumberOfPeople? NumberOfPeople { get; set; }
        public List<Tags>? Tags { get; set; }

        public string? OwnerReply { get; set; }
        public DateTime? OwnerRepliedAt { get; set; }
        public DateTime? OwnerReplyEditedAt { get; set; }
    }

}