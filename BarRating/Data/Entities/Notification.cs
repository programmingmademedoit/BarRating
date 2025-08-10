namespace BarRating.Data.Entities
{
    public class Notification : BaseEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsRead { get; set; }
    }
}
