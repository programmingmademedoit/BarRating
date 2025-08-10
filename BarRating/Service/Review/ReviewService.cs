
using BarRating.Data.Entities;
using BarRating.Data.Enums;
using BarRating.Models.Review;
using BarRating.Repository;
using BarRating.Service.HelpfulVote;
using BarRating.Service.SavedBar;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarRating.Service.Review
{
    public class ReviewService : IReviewService
    {
        private readonly ReviewRepository repository;
        private readonly BarRepository barRepository;
        private readonly UserRepository userRepository;
        private readonly IHelpfulVoteService helpfulVoteService;
        private readonly SavedBarRepository savedBarRepository;
        private readonly ISavedBarService savedBarService;

        public ReviewService(ReviewRepository repository,
            BarRepository barRepository,
            UserRepository userRepository,
            IHelpfulVoteService helpfulVoteService,
            SavedBarRepository savedBarRepository,
            ISavedBarService savedBarService)
        {
            this.repository = repository;
            this.barRepository = barRepository;
            this.userRepository = userRepository;
            this.helpfulVoteService = helpfulVoteService;
            this.savedBarRepository = savedBarRepository;
            this.savedBarService = savedBarService;
        }
        public List<ReviewViewModel> GetSpecifyPage(List<Data.Entities.Review> reviews, int userId)
        {
            return reviews.Select(r => new ReviewViewModel
            {
                BarId = r.BarId,
                ReviewId = r.Id,
                CreatedById = r.CreatedById,
                Text = r.Text,
                Rating = r.Rating,
                Price = r.Price,
                NumberOfPeople = r.NumberOfPeople,
                Tags = r.Tags,
                OwnerReply = r.OwnerReply,
                OwnerRepliedAt = r.OwnerRepliedAt,
                OwnerReplyEditedAt = r.OwnerReplyEditedAt,
                IsHelpfulByCurrentUser = userId != 0 && r.HelpfulVotes?.Any(h => h.CreatedById == userId) == true,
                HelpfulCount = r.HelpfulVotes?.Count() ?? 0,
                CreatedOn = r.CreatedOn,
                EditedAt = r.EditedAt,
                UserName = r.CreatedBy?.UserName,
                UserRank = r.CreatedBy?.Rank ?? Rank.Newbie
            }).ToList();
        }

        public async Task<Data.Entities.Review> Create(CreateReviewViewModel model, Data.Entities.User createdBy)
        {
            Data.Entities.Bar bar = barRepository.GetBarById(model.BarId);
            Data.Entities.Review review = new Data.Entities.Review
            {
                BarId = model.BarId,
                Text = model.Text,
                Rating = model.Rating,
                Price = model.Price,
                NumberOfPeople = model.NumberOfPeople,
                Tags = model.Tags,
                CreatedBy = createdBy,
                CreatedById = createdBy.Id,
                CreatedOn = DateTime.UtcNow,
            };

            return await repository.Create(review);
        }
        public async Task<EditReviewViewModel> GetEdit(int reviewId)
        {
            Data.Entities.Review review = repository.GetReviewById(reviewId);
            EditReviewViewModel model = new EditReviewViewModel
            {
                Id = review.Id,
                BarId = review.BarId,
                Text = review.Text,
                Rating = review.Rating,
                Price = review.Price,
                NumberOfPeople = review.NumberOfPeople,
                Tags = review.Tags,

            };
            return model;
        }
        public async Task<Data.Entities.Review> PostEdit(EditReviewViewModel model)
        {
            Data.Entities.Review review = repository.GetReviewById(model.Id);
            review.Text = model.Text;
            review.Rating = model.Rating;
            review.EditedAt = DateTime.UtcNow;
            review.Price = model.Price;
            review.NumberOfPeople = model.NumberOfPeople;
            review.Tags = model.Tags;
            return await repository.Edit(review);
        }
        public async Task<Data.Entities.Review> Delete(Data.Entities.Review review)
        {
            return await repository.Delete(review);
        }
        public async Task<List<ReviewViewModel>> GetUserReviews(int userId)
        {
            List<Data.Entities.Review> reviews = repository.GetUserReviews(userId);
            return reviews.Select(r => new ReviewViewModel
            {
                BarId = r.BarId,
                BarName = r.Bar.Name,
                BarImage = r.Bar.Image,
                ReviewId = r.Id,
                CreatedById = userId,
                Text = r.Text,
                Rating = r.Rating,
                Price = r.Price,
                NumberOfPeople = r.NumberOfPeople,
                Tags = r.Tags,
                OwnerId = r.Bar.OwnerId,
                OwnerReply = r.OwnerReply,
                OwnerRepliedAt = r.OwnerRepliedAt,
                OwnerReplyEditedAt = r.OwnerReplyEditedAt,
                HelpfulCount = r.HelpfulVotes?.Count() ?? 0,
                CreatedOn = r.CreatedOn,
                EditedAt = r.EditedAt,
                UserName = r.CreatedBy?.UserName,
                UserRank = r.CreatedBy?.Rank ?? Rank.Newbie
            }).ToList();
        }
    }
}
