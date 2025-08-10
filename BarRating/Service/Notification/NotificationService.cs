using BarRating.Data.Helpers;
using BarRating.Models.Notification;
using BarRating.Repository;
using Microsoft.AspNetCore.SignalR;

namespace BarRating.Service.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationRepository notificationRepository;
        private readonly UserRepository userRepository;
        private readonly IHubContext<NotificationHub> hubContext;
        public NotificationService(
            NotificationRepository notificationRepository,
            UserRepository userRepository,
            IHubContext<NotificationHub> hubContext)
        {
            this.notificationRepository = notificationRepository;
            this.userRepository = userRepository;
            this.hubContext = hubContext;
        }
        public List<NotificationViewModel> GetUserNotifications(int userId)
        {
            List<Data.Entities.Notification> notifications = notificationRepository.GetUserNotifications(userId);
            return notifications.Select(n => new NotificationViewModel
            { 
                UserId = userId,
                Text = n.Text,
                IsRead = n.IsRead,
                CreatedOn = n.CreatedOn,
            }).ToList();
        }
        public async Task<Data.Entities.Notification> Create(string text, int userId)
        {
            Data.Entities.Notification notification = new Data.Entities.Notification()
            { 
                User = userRepository.GetUserById(userId),
                UserId = userId,
                Text = text,
                IsRead = false,
                CreatedOn = DateTime.UtcNow
            };
            var createNotification = await notificationRepository.Create(notification);
            await hubContext.Clients.User(userId.ToString())
                         .SendAsync("ReceiveNotification", text);
            return createNotification;
        }

        public async Task<Data.Entities.Notification> Delete(int notificationId)
        {
            Data.Entities.Notification notification = notificationRepository.GetNotificationbyId(notificationId);
            return await notificationRepository.Delete(notification);
        }
    }
}
