using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarRating.Repository
{
    public class NotificationRepository : CommonRepository<Notification>
    {
        public NotificationRepository(ApplicationDbContext context) : base(context) { }
        public List<Notification> GetUserNotifications(int  userId)
        {
            return context.Notifications
                .Include(n => n.User)
                .Where(n => n.UserId == userId)
                .ToList();
        }
        public Notification GetNotificationbyId(int id)
        {
            return context.Notifications
                .Include(n => n.User)
                .Single(n => n.Id == id);
        }
    }
}
