
using BarRating.Data.Entities;

namespace BarRating.Data.Entities
{
    public class Review : MetadataBaseEntity
    {
        public int BarId { get; set; }
        public Bar Bar { get; set; }

        public string Text { get; set; }
        public int Rating { get; set; }

        public bool WasThisHelpful { get; set; }
        public bool IsEdited { get; set; } = false;
        public DateTime? EditedAt { get; set; }
        public PriceCategory Price { get; set; }
        public int? NumberOfPeople { get; set; }
        public bool IsOwner { get; set; } = false;
        public List<Tags> Tags { get; set; }

        // New: navigation property for comments
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
    }

    public class Comment : MetadataBaseEntity
    {

        public int ReviewId { get; set; }
        public Review Review { get; set; }

        public string Text { get; set; }

        public bool IsEdited { get; set; } = false;
        public DateTime? EditedAt { get; set; }
    }
}