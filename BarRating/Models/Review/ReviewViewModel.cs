namespace BarRating.Models.Review
{
    public class ReviewViewModel
    {
        public int BarId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<IndexReviewViewModel> Reviews { get; set; }
    }
}
