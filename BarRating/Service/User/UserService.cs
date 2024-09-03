using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Models.User;
using BarRating.Repository;

namespace BarRating.Service.User
{
    public class UserService
    {
        private readonly UserRepository useRepository;
        private readonly ApplicationDbContext context;
        public UserService(UserRepository userRepository,
                           ApplicationDbContext context) 
        {
        this.useRepository = userRepository;
        this.context = context;
        }
        
    }
}
