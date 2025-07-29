
using System.ComponentModel.DataAnnotations;

namespace BarRating.Models.User
{
    public class LoginViewModel : AuthenticationBasicRequest
    {
            [Required]
            public string Username { get; set; } = string.Empty;

            [Required]
        public string Password { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
        
    }
}
