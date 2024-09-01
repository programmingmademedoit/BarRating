using BarRating.Data;
using BarRating.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarRating.Repository
{
    public class UserRepository 
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public User GetUserById(int userId)
        {
            return _context.Users.Where(u => u.Id == userId).Single();
        }
    }
}
