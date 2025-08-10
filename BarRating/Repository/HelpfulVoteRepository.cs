using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarRating.Repository
{
    public class HelpfulVoteRepository : CommonRepository<HelpfulVote>
    {
        public HelpfulVoteRepository(ApplicationDbContext context) : base(context) { }
        public HelpfulVote GetHelpfulVoteById(int id)
        {
            return context.HelpfulVotes
                .Include(h => h.CreatedBy)
                .Include(h => h.Review)
                .Single(h => h.Id == id);
        }
        public HelpfulVote GetVoteByReviewIdandUserId(int reviewId, int userId)
        {
            return context.HelpfulVotes
        .Include(h => h.CreatedBy)
        .Include(h => h.Review)
        .SingleOrDefault(h => h.ReviewId == reviewId && h.CreatedById == userId);
        }
    }
}
