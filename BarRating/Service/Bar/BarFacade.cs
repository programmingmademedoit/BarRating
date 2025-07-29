using BarRating.Repository;
using BarRating.Service.Photo;
using BarRating.Service.Review;
using Microsoft.AspNetCore.Identity;

namespace BarRating.Service.Bar
{
    public class BarFacade : IBarFacade
    {
        private readonly IBarService barService;
        private readonly IPhotoService photoService;
        private readonly IReviewService reviewService;
        private readonly UserManager<Data.Entities.User> userManager;
        private readonly UserRepository userRepository;
        private readonly BarRepository barRepository;
        public BarFacade(
            IBarService barService,
            IPhotoService photoService,
            IReviewService reviewService,
            UserManager<Data.Entities.User> userManager,
            UserRepository userRepository,
            BarRepository barRepository)
        {
            this.barService = barService;
            this.photoService = photoService;
            this.reviewService = reviewService;
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.barRepository = barRepository;
        }

    }
}
