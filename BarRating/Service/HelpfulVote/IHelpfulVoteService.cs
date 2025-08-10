using BarRating.Models.HelpfulVote;

namespace BarRating.Service.HelpfulVote
{
    public interface IHelpfulVoteService
    {
        public List<HelpfulVoteViewModel> GetHelpfulVoteViewModel(List<Data.Entities.HelpfulVote> helpfulVotes);
        public Task<Data.Entities.HelpfulVote> Create(int reviewId, int userId);
        public Task<Data.Entities.HelpfulVote> Delete(int reviewId, int userId);
        public bool HasUserVoted(int reviewId, int userId);
    }
}