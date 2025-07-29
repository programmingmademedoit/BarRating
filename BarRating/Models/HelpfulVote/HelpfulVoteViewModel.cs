namespace BarRating.Models.HelpfulVote
{
    public class HelpfulVoteViewModel
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
