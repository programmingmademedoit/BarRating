using BarRating.Models.Notification;

namespace BarRating.Service.Notification
{
    public interface INotificationService
    {
        public List<NotificationViewModel> GetUserNotifications(int userId);
        public Task<Data.Entities.Notification> Create(string text, int userId);
        public Task<Data.Entities.Notification> Delete(int notificationId);
    }
}