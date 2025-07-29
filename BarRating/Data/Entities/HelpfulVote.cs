namespace BarRating.Data.Entities
{
    public class HelpfulVote : MetadataBaseEntity
    {
        public int ReviewId { get; set; }
        public Review Review { get; set; }

    }
}
