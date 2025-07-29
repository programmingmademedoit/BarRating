using System.ComponentModel.DataAnnotations;

namespace BarRating.Models.Reply
{
    public class ReplyViewModel
    {
        public int BarId { get; set; }
        public int ReviewId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Reply cannot exceed 500 characters.")]
        public string OwnerReply { get; set; }
    }
}