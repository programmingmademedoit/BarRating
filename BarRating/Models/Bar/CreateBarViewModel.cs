using BarRating.Data.Entities;

namespace BarRating.Models.Bar
{
    public class CreateBarViewModel
    {
        public int BarId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public List<Data.Entities.Review> Reviews { get; set; } = new List<Data.Entities.Review>();
    }
}
