using BarRating.Data;
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
                                .Include(r => r.Restaurant)
                                .Include(r => r.CreatedBy)
                                .Single(b => b.Id == id);
         
            return review;
        }
        public List<Data.Entities.Review> GetAllUserReviews(int userId)
        {
            return context.Reviews
                          .Where(r => r.CreatedById == userId)
                          .Include(r => r.Restaurant)
                          .Include(r => r.CreatedBy)
                          .ToList();
        }



    }
}

