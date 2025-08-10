namespace BarRating.Models.Notification
{
    public class NotificationViewModel
    {
        public int UserId { get; set; }
        public string Text { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
