using BarRating.Data.Entities;
using BarRating.Models.Review;

namespace BarRating.Service.Review
{
    public interface IReviewService
    {
        public List<ReviewViewModel> GetSpecifyPage(List<Data.Entities.Review> reviews, int userId);

        public Task<Data.Entities.Review> Create(CreateReviewViewModel model, Data.Entities.User createdBy);
        public Task<EditReviewViewModel> GetEdit(int reviewId);
        public Task<Data.Entities.Review> PostEdit(EditReviewViewModel model);
        public Task<Data.Entities.Review> Delete(Data.Entities.Review review);
    }
}
