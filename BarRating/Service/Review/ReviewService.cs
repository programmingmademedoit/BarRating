using BarRating.Data.Entities;
using BarRating.Models.Review;
using BarRating.Repository;

namespace BarRating.Service.Review
{
    public class ReviewService : IReviewService
    {
        private readonly ReviewRepository repository;
        public ReviewService(ReviewRepository repository)
        {
            this.repository = repository;
        }
        public IndexReviewViewModel GetSpecifyPage(Data.Entities.Review review)
        {
            IndexReviewViewModel model = new IndexReviewViewModel
            {
                
                Id = review.Id,
                Text = review.Text,
                Author = review.CreatedBy           
            };
            return model;
        }
        public async Task<Data.Entities.Review> Create(Data.Entities.Review review,Data.Entities.User user)
        {
            review.CreatedBy = user;
            return await repository.Create(review);
        }
        public async Task<Data.Entities.Review> Edit(Data.Entities.Review review)
        {
            return await repository.Edit(review);
        }
        public async Task<Data.Entities.Review> Delete(Data.Entities.Review review)
        {
            return await repository.Delete(review);
        }
    }
}
