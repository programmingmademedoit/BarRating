using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Models.Account;
using BarRating.Models.Admin;
using BarRating.Repository;
using BarRating.Service.Review;
using BarRating.Service.SavedBar;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace BarRating.Service.User
{
    public class UserService : IUserService
    {
        private readonly UserRepository userRepository;
        private readonly ApplicationDbContext context;
        private readonly UserManager<Data.Entities.User> userManager;
        private readonly RoleManager<Data.Entities.Role> roleManager;
        private readonly ISavedBarService savedBarService;
        private readonly IReviewService reviewService;
        public UserService(UserRepository userRepository,
                           ApplicationDbContext context,
                           UserManager<Data.Entities.User> userManager,
                           RoleManager<Data.Entities.Role> roleManager,
                           ISavedBarService savedBarService,
                           IReviewService reviewService)
        {
            this.userRepository = userRepository;
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.savedBarService = savedBarService;
            this.reviewService = reviewService;
        }
        public async Task<EditUserRoleViewModel> EditRole(int userId)
        {
            var user = userRepository.GetUserById(userId);

            var userRoleNames = await userManager.GetRolesAsync(user);

            var userRoleIds = roleManager.Roles
                .Where(r => userRoleNames.Contains(r.Name))
                .Select(r => r.Id)
                .ToList();

            var allRoles = roleManager.Roles.ToList();

            var model = new EditUserRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserRoles = userRoleIds,
                AllRoles = allRoles
            };
            return model;
        }
        public async Task<AccountIndexViewModel> Index(int userId)
        {
            Data.Entities.User user = userRepository.GetUserById(userId);
            AccountIndexViewModel model = new AccountIndexViewModel()
            { 
               UserId = user.Id,
               UserName = user.UserName,
               //RegistrationDate = user.CreatenOn
               Rank = user.Rank,
               IsVerified = user.IsVerified,
            };
            return model;

        }
        public async Task<AccountEditViewModel> EditFirstAndLastName(int userId)
        {
            Data.Entities.User user = userRepository.GetUserById(userId);
            AccountEditViewModel model = new AccountEditViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
            return model;
        }
        public async Task<AccountEditViewModel> EditUserName(int userId)
        {
            Data.Entities.User user = userRepository.GetUserById(userId);
            AccountEditViewModel model = new AccountEditViewModel()
            {
                UserName = user.UserName
            };
            return model;
        }
        public async Task<AccountEditViewModel> EditEmail(int userId)
        {
            Data.Entities.User user = userRepository.GetUserById(userId);
            AccountEditViewModel model = new AccountEditViewModel()
            {
                Email = user.Email
            };
            return model;
        }
        public async Task<AccountEditViewModel> EditPassword(int userId)
        {
            Data.Entities.User user = userRepository.GetUserById(userId);
            AccountEditViewModel model = new AccountEditViewModel()
            {
                Password = user.PasswordHash
            };
            return model;
        }
        public async Task<AccountEditViewModel> EditProfile(int userId)
        {
            Data.Entities.User user = userRepository.GetUserById(userId);
            AccountEditViewModel model = new AccountEditViewModel()
            { 
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.PasswordHash
            };
            return model;

        }
    }        
}
