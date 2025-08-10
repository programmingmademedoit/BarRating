using BarRating.Models.Account;
using BarRating.Models.Admin;

namespace BarRating.Service.User
{
    public interface IUserService
    {
        public Task<EditUserRoleViewModel> EditRole(int userId);
        public Task<AccountIndexViewModel> Index(int userId);
        public Task<AccountEditViewModel> EditFirstAndLastName(int userId);
        public Task<AccountEditViewModel> EditUserName(int userId);
        public Task<AccountEditViewModel> EditEmail(int userId);
        public Task<AccountEditViewModel> EditPassword(int userId);
        public Task<AccountEditViewModel> EditProfile(int userId);
    }
}
