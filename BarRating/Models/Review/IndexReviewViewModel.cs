namespace BarRating.Models.Review
{
    public class IndexReviewViewModel
    {
        public int Id { get; set; }
        public Data.Entities.User Author { get; set; }
        public string Text { get; set; }
        
    }
}
