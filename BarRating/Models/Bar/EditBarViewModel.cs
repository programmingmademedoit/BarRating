using BarRating.Data.Entities;

namespace BarRating.Models.Bar
{
    public class EditBarViewModel
    {
        public int BarId { get; set; }
        public int CreatedById { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public string URL { get; set; }
    }
}
