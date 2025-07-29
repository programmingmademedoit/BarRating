using BarRating.Models.HelpfulVote;
using BarRating.Repository;

namespace BarRating.Service.HelpfulVote
{
    public class HelpfulVoteService : IHelpfulVoteService
    {
        private readonly HelpfulVoteRepository helpfulVoteRepository;
        public HelpfulVoteService(HelpfulVoteRepository helpfulVoteRepository)
        {
            this.helpfulVoteRepository = helpfulVoteRepository;
        }
        public List<HelpfulVoteViewModel> GetHelpfulVoteViewModel(List<Data.Entities.HelpfulVote> helpfulVotes)
        {
            return helpfulVotes.Select(h => new HelpfulVoteViewModel
            {
                Id = h.Id,
                ReviewId = h.ReviewId,
                UserId = h.CreatedById,
                CreatedOn = h.CreatedOn
            }).ToList();
        }
        public async Task<Data.Entities.HelpfulVote> Create(int reviewId, int userId)
        {
            Data.Entities.HelpfulVote helpfulVote = new Data.Entities.HelpfulVote
            {
                ReviewId = reviewId,
                CreatedById = userId,
                CreatedOn = DateTime.UtcNow
            };
            return await helpfulVoteRepository.Create(helpfulVote);
        }
        public async Task<Data.Entities.HelpfulVote> Delete(int reviewId, int userId)
        {
            Data.Entities.HelpfulVote helpfulVote = helpfulVoteRepository.GetVoteByReviewIdandUserId(reviewId, userId);
            return await helpfulVoteRepository.Delete(helpfulVote);
        }
        public bool HasUserVoted(int barId, int userId)
        {
            var savedBar = helpfulVoteRepository.GetVoteByReviewIdandUserId(barId, userId);
            return savedBar != null;
        }
    }
}
