using BarRating.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarRating.Data.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Rank Rank { get; set; }
        public bool IsVerified { get; set; } = false;
        public ICollection<SavedBar> SavedBars { get; set; } = new List<SavedBar>();
        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? URL { get; set; }
    }
}
