namespace BarRating.Models.Review
{
    public class CreateReviewViewModel
    {
        public int Id { get; set; }
        public int BarId { get; set; }
        public string Text { get; set; }
    }
}
