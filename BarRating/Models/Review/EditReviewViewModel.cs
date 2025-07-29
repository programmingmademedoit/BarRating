
using BarRating.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace BarRating.Models.Review
{
    public class EditReviewViewModel
    {
        public int Id { get; set; }

        [Required]
        public int BarId { get; set; }
        [Required] 
        public int CreatedById { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Review text cannot exceed 1000 characters.")]
        public string Text { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        public PriceCategory? Price { get; set; }

        [Range(1, 50, ErrorMessage = "Number of people must be between 1 and 50.")]
        public NumberOfPeople? NumberOfPeople { get; set; }

        public List<Tags>? Tags { get; set; }
    }
}