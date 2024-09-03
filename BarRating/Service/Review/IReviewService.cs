using BarRating.Data.Entities;
using BarRating.Models.Review;

namespace BarRating.Service.Review
{
    public interface IReviewService
    {
        public IndexReviewViewModel GetSpecifyPage(Data.Entities.Review review);

        public Task<Data.Entities.Review> Create(Data.Entities.Review review, Data.Entities.User user);
        public Task<Data.Entities.Review> Edit(Data.Entities.Review review);
        public Task<Data.Entities.Review> Delete(Data.Entities.Review review);
    }
}
