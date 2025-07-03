using BarRating.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace BarRating.Models.Bar
{
    public class CreateBarViewModel
    {
        public int BarId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public List<Data.Entities.Review> Reviews { get; set; } = new List<Data.Entities.Review>();
    }
}
