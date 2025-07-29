using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Models.User;
using BarRating.Repository;
using Microsoft.AspNetCore.Identity;

namespace BarRating.Service.User
{
    public class UserService : IUserService
    {
        private readonly UserRepository userRepository;
        private readonly ApplicationDbContext context;
        private readonly UserManager<Data.Entities.User> userManager;
        private readonly RoleManager<Data.Entities.Role> roleManager;
        public UserService(UserRepository userRepository,
                           ApplicationDbContext context,
                           UserManager<Data.Entities.User> userManager,
                           RoleManager<Data.Entities.Role> roleManager) 
        {
        this.userRepository = userRepository;
        this.context = context;
        this.userManager = userManager;
        this.roleManager = roleManager;
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
    }
}
