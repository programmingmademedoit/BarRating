using BarRating.Models.User;

namespace BarRating.Service.User
{
    public interface IUserService
    {
        public Task<EditUserRoleViewModel> EditRole(int userId);
    }
}
