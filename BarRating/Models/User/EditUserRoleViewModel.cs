using BarRating.Data.Entities;

namespace BarRating.Models.User
{
    public class EditUserRoleViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<int> UserRoles { get; set; }
        public List<Role> AllRoles { get; set; }
        public IEnumerable<int> SelectedRoles { get; set; }
    }
}

