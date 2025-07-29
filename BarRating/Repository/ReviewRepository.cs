using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarRating.Repository
{
    public class ReviewRepository : CommonRepository<Data.Entities.Review>
    {
        public ReviewRepository(ApplicationDbContext context) : base(context) { }
        public Data.Entities.Review GetReviewById(int id)
        {
            var review = context.Reviews
                                .Include(r => r.Bar)
                                .Include(r => r.CreatedBy)
                                .Single(b => b.Id == id);
         
            return review;
        }
        public List<Data.Entities.Review> GetAllUserReviews(int Id)
        {
            return context.Reviews
                          .Include(r => r.Bar)
                          .Where(r => r.CreatedById == Id)
                          .ToList();
        }
        public async Task<List<Review>> Search(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return await context.Reviews.ToListAsync();
            }

            return await context.Reviews
                                .Where(b => b.Text.ToLower().StartsWith(searchQuery.ToLower()))
                                .ToListAsync();
        }



    }
}

